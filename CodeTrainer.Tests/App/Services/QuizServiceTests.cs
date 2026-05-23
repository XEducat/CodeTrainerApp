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
    public class QuizServiceTests
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly HttpClient _httpClient;
        private readonly QuizService _service;

        public QuizServiceTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost/")
            };
            _service = new QuizService(_httpClient);
        }

        [Fact]
        public async Task GetAllQuizzesAsync_ShouldReturnQuizzes()
        {
            // Arrange
            var quizzes = new List<Quiz>
            {
                new Quiz { Id = 1, Title = "Q1", Description = "D1" },
                new Quiz { Id = 2, Title = "Q2", Description = "D2" }
            };
            var json = JsonSerializer.Serialize(quizzes);

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
            var result = await _service.GetAllQuizzesAsync(5);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Q1", result[0].Title);
        }

        [Fact]
        public async Task AddQuizAsync_WithValidQuiz_ShouldReturnSuccess()
        {
            // Arrange
            var quiz = new Quiz { Title = "New", Description = "Desc" };
            var responseQuiz = new Quiz { Id = 10, Title = "New", Description = "Desc" };
            var json = JsonSerializer.Serialize(responseQuiz);

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
            var (success, message, created) = await _service.AddQuizAsync(quiz);

            // Assert
            Assert.True(success);
            Assert.NotNull(created);
            Assert.Equal(10, created.Id);
        }
    }
}
