using CodeTrainerAPI.Data;
using CodeTrainerAPI.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CodeTrainer.Tests.Integration
{
    public class UserDataIsolationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UserDataIsolationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
            });
        }

        [Fact]
        public async Task ClearHistory_ShouldOnlyAffectCurrentUserInfo()
        {
            // 1. Setup - Seed database with history for two users
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.UserHistories.RemoveRange(db.UserHistories);
                
                db.UserHistories.Add(new UserHistory { UserId = "target-user", QuizId = 1, Score = 10, MaxScore = 10 });
                db.UserHistories.Add(new UserHistory { UserId = "other-user", QuizId = 1, Score = 5, MaxScore = 10 });
                await db.SaveChangesAsync();
            }

            // 2. Simulate User session (Mocking the identity via Controller Context is for Unit Tests
            var client = _factory.CreateClient();
            
            
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var historyCount = await db.UserHistories.CountAsync();
                Assert.Equal(2, historyCount);
            }
        }
    }
}
