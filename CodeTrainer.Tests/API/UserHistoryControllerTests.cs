using CodeTrainerAPI.Controllers;
using CodeTrainerAPI.Data;
using CodeTrainerAPI.Data.Models;
using CodeTrainerAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Moq;
using Xunit;

namespace CodeTrainer.Tests.API
{
    public class UserHistoryControllerTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetMyHistory_ShouldReturnUserHistory()
        {
            // Arrange
            var context = GetDbContext();
            string userId = "user123";
            var quiz = new Quiz { Id = 1, Title = "C# Basics", MentorId = "mentor1", Description = "Desc" };
            context.Quizzes.Add(quiz);
            context.UserHistories.Add(new UserHistory { Id = 1, UserId = userId, QuizId = 1, Score = 5, MaxScore = 5 });
            context.UserHistories.Add(new UserHistory { Id = 2, UserId = "otherUser", QuizId = 1, Score = 2, MaxScore = 5 });
            await context.SaveChangesAsync();

            var logger = new Mock<ILogger<UserHistoryController>>();
            var controller = new UserHistoryController(context, logger.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = controller.GetMyHistory();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var history = Assert.IsAssignableFrom<List<UserHistoryDto>>(okResult.Value);
            Assert.Single(history);
            Assert.Equal("C# Basics", history[0].QuizTitle);
        }

        [Fact]
        public async Task CreateHistory_ShouldSaveNewRecord()
        {
            // Arrange
            var context = GetDbContext();
            var logger = new Mock<ILogger<UserHistoryController>>();
            var controller = new UserHistoryController(context, logger.Object);
            string userId = "user123";

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var model = new UserHistory { QuizId = 1, Score = 8, MaxScore = 10, UserId = "" }; // UserId will be set by controller

            // Act
            var result = controller.CreateHistory(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var saved = Assert.IsType<UserHistory>(okResult.Value);
            Assert.Equal(userId, saved.UserId);
            Assert.Equal(8, saved.Score);
            Assert.Equal(1, await context.UserHistories.CountAsync());
        }

        [Fact]
        public async Task ClearHistory_ShouldRemoveAllUserRecords()
        {
            // Arrange
            var context = GetDbContext();
            string userId = "user123";
            context.UserHistories.Add(new UserHistory { UserId = userId, QuizId = 1, Score = 5, MaxScore = 5 });
            context.UserHistories.Add(new UserHistory { UserId = userId, QuizId = 2, Score = 3, MaxScore = 5 });
            context.UserHistories.Add(new UserHistory { UserId = "other", QuizId = 1, Score = 1, MaxScore = 5 });
            await context.SaveChangesAsync();

            var logger = new Mock<ILogger<UserHistoryController>>();
            var controller = new UserHistoryController(context, logger.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            await controller.ClearHistory();

            // Assert
            Assert.Equal(1, await context.UserHistories.CountAsync()); // Only the other user's record remains
        }
    }
}
