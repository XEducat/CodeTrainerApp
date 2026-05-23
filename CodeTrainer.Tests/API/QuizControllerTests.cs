using CodeTrainerAPI.Controllers;
using CodeTrainerAPI.Data;
using CodeTrainerAPI.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Xunit;

namespace CodeTrainer.Tests.API
{
    public class QuizControllerTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetQuizzes_ShouldReturnAllQuizzes()
        {
            // Arrange
            var context = GetDbContext();
            context.Quizzes.Add(new Quiz { Id = 1, Title = "Quiz 1", MentorId = "mentor1", Description = "Desc 1" });
            context.Quizzes.Add(new Quiz { Id = 2, Title = "Quiz 2", MentorId = "mentor1", Description = "Desc 2" });
            await context.SaveChangesAsync();

            var controller = new QuizController(context);

            // Act
            var result = await controller.GetQuizzes();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Quiz>>>(result);
            var quizzes = Assert.IsAssignableFrom<IEnumerable<Quiz>>(actionResult.Value);
            Assert.Equal(2, (quizzes as List<Quiz>).Count);
        }

        [Fact]
        public async Task GetQuiz_WithValidId_ShouldReturnQuiz()
        {
            // Arrange
            var context = GetDbContext();
            var quiz = new Quiz { Id = 1, Title = "Target Quiz", MentorId = "mentor1", Description = "Target Desc" };
            context.Quizzes.Add(quiz);
            await context.SaveChangesAsync();

            var controller = new QuizController(context);

            // Act
            var result = await controller.GetQuiz(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Quiz>>(result);
            var returnedQuiz = Assert.IsType<Quiz>(actionResult.Value);
            Assert.Equal("Target Quiz", returnedQuiz.Title);
        }

        [Fact]
        public async Task GetMyQuizzes_ShouldReturnOnlyMentorQuizzes()
        {
            // Arrange
            var context = GetDbContext();
            context.Quizzes.Add(new Quiz { Id = 1, Title = "Mentor 1 Quiz", MentorId = "mentor1", Description = "Desc 1" });
            context.Quizzes.Add(new Quiz { Id = 2, Title = "Mentor 2 Quiz", MentorId = "mentor2", Description = "Desc 2" });
            await context.SaveChangesAsync();

            var controller = new QuizController(context);
            
            // Mocking User identity
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "mentor1"),
                new Claim(ClaimTypes.Role, "Mentor")
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            // Act
            var result = await controller.GetMyQuizzes();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Quiz>>>(result);
            var quizzes = Assert.IsAssignableFrom<IEnumerable<Quiz>>(actionResult.Value);
            var list = quizzes as List<Quiz>;
            Assert.Single(list);
            Assert.Equal("Mentor 1 Quiz", list[0].Title);
        }

        [Fact]
        public async Task CreateQuiz_ShouldAddQuizToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new QuizController(context);
            var newQuiz = new Quiz { Title = "New Quiz", MentorId = "mentor1", Description = "Desc" };

            // Act
            var result = await controller.CreateQuiz(newQuiz);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdQuiz = Assert.IsType<Quiz>(actionResult.Value);
            Assert.Equal("New Quiz", createdQuiz.Title);
            Assert.Equal(1, await context.Quizzes.CountAsync());
        }
    }
}
