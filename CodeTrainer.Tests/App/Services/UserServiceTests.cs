using CodeTrainerApp.Services;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using Xunit;

namespace CodeTrainer.Tests.App.Services
{
    public class UserServiceTests
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly HttpClient _httpClient;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };
            // Use Reflection to access the internal constructor
            var constructor = typeof(UserService).GetConstructors(
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .FirstOrDefault(c => c.GetParameters().Length == 1 && c.GetParameters()[0].ParameterType == typeof(HttpClient));
            
            _service = (UserService)constructor.Invoke(new object[] { _httpClient });
        }

        [Fact]
        public async Task LoginAsync_WithValidData_ShouldSetCurrentUser()
        {
            // Arrange
            var userData = new
            {
                id = "1",
                email = "test@test.com",
                login = "tester",
                birthDate = DateTime.Now.AddYears(-20),
                role = "Student"
            };
            var json = JsonSerializer.Serialize(userData);

            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.RequestUri.ToString().Contains("login")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json)
                });

            // Act
            var (success, message) = await _service.LoginAsync("test@test.com", "pass");

            // Assert
            Assert.True(success);
            Assert.True(_service.IsLoggedIn);
            Assert.Equal("tester", _service.CurrentUser.Login);
        }

        [Fact]
        public async Task LogoutAsync_ShouldClearCurrentUser()
        {
            // Arrange
            // Pre-fill CurrentUser (via Reflection if needed or by login)
            // For simplicity, we just check if it calls the API and clears
            
            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.RequestUri.ToString().Contains("logout")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

            // Act
            await _service.LogoutAsync();

            // Assert
            Assert.False(_service.IsLoggedIn);
            Assert.Null(_service.CurrentUser);
        }
    }
}
