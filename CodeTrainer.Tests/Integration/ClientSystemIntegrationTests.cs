using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using Xunit;

namespace CodeTrainer.Tests.Integration
{
    public class ClientSystemIntegrationTests
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly HttpClient _httpClient;
        private readonly UserService _userService;
        private readonly UserHistoryService _historyService;

        public ClientSystemIntegrationTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };

            // Setup UserService via Reflection
            var constructor = typeof(UserService).GetConstructors(
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .FirstOrDefault(c => c.GetParameters().Length == 1 && c.GetParameters()[0].ParameterType == typeof(HttpClient));
            _userService = (UserService)constructor.Invoke(new object[] { _httpClient });

            _historyService = new UserHistoryService(_httpClient);
        }

        [Fact]
        public async Task SystemFlow_SolveTask_AndSaveHistory_ShouldWork()
        {
            // 1. Simulate Login
            var userData = new { id = "user1", email = "test@test.com", login = "tester", birthDate = DateTime.Now, role = "Student" };
            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(m => m.RequestUri.ToString().Contains("login")), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonSerializer.Serialize(userData)) });

            await _userService.LoginAsync("test@test.com", "password");

            // 2. Simulate Solving Code (Compiler)
            var task = new ProgrammingTask
            {
                Id = 1,
                Title = "Test",
                Tests = new List<TestCase> { new TestCase { Call = "new Solution().Test()", Expected = "1" } }
            };
            string userCode = "public class Solution { public int Test() { return 1; } }";
            var runResult = await CodeCompiler.RunCode(task, userCode);
            Assert.True(runResult.success);

            // 3. Simulate Saving result to API
            var historyRecord = new UserHistory { QuizId = 1, Score = 1, MaxScore = 1, QuizTitle = "Test Quiz", CompletedAt = DateTime.UtcNow };
            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Post && m.RequestUri.ToString().Contains("UserHistory/create")), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("{\"id\": 100}") });

            var savedHistory = await _historyService.CreateHistoryAsync(historyRecord, _userService.CurrentUser.Id);

            // Assert
            Assert.NotNull(savedHistory);
            Assert.Equal(100, savedHistory.Id);
            Assert.True(_userService.IsLoggedIn);
        }
    }
}
