using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CodeTrainer.Tests.App.Services
{
    public class UserHistoryServiceTests
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly HttpClient _httpClient;
        private readonly UserHistoryService _service;

        public UserHistoryServiceTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };
            _service = new UserHistoryService(_httpClient);
        }

        [Fact]
        public async Task GetUserHistoryAsync_ShouldReturnHistory()
        {
            // Arrange
            var history = new List<UserHistory>
            {
                new UserHistory { Id = 1, QuizTitle = "Test Quiz", Score = 10, MaxScore = 10 }
            };
            var json = JsonSerializer.Serialize(history);

            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json)
                });

            // Act
            var result = await _service.GetUserHistoryAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Test Quiz", result[0].QuizTitle);
        }

        [Fact]
        public async Task CreateHistoryAsync_ShouldPostDataAndReturnResult()
        {
            // Arrange
            var attempt = new UserHistory { QuizId = 1, Score = 5, MaxScore = 5, QuizTitle = "Q" };
            var responseJson = "{\"id\": 1, \"userId\": \"u1\", \"quizId\": 1, \"score\": 5}";

            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseJson)
                });

            // Act
            var result = await _service.CreateHistoryAsync(attempt, "u1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("u1", result.UserId);
        }
    }
}
