using CodeTrainerAPI.Data;
using CodeTrainerAPI.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace CodeTrainer.Tests.Integration
{
    public class QuizLifecycleIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public QuizLifecycleIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "Test";
                        options.DefaultChallengeScheme = "Test";
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            });
        }

        [Fact]
        public async Task Mentor_CreateQuiz_AndStudent_CanSeeIt()
        {
            // 1. Setup - Create a Client and Database state
            var client = _factory.CreateClient();
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Quizzes.RemoveRange(db.Quizzes); // Clear
                await db.SaveChangesAsync();
            }

            // 2. Mentor adds a quiz via API
            var newQuiz = new Quiz 
            { 
                Title = "Integration C#", 
                Description = "Testing the full flow", 
                MentorId = "mentor-1",
                Tasks = new List<ProgrammingTask>
                {
                    new ProgrammingTask { Title = "Task 1", Description = "Desc", CodeTemplate = "..." }
                }
            };
            var createResponse = await client.PostAsJsonAsync("/api/Quiz", newQuiz);
            createResponse.EnsureSuccessStatusCode();

            // 3. Student fetches quizzes
            var getResponse = await client.GetAsync("/api/Quiz");
            var quizzes = await getResponse.Content.ReadFromJsonAsync<List<Quiz>>();

            // Assert
            Assert.NotNull(quizzes);
            Assert.Contains(quizzes, q => q.Title == "Integration C#");
            Assert.Single(quizzes[0].Tasks);
        }

        [Fact]
        public async Task GetQuizById_NonExistent_ShouldReturnNotFound()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/Quiz/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
