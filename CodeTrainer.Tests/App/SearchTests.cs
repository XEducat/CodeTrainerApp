using CodeTrainerApp.Model;
using Xunit;

namespace CodeTrainer.Tests.App
{
    public class SearchTests
    {
        private List<Quiz> GetSampleQuizzes()
        {
            return new List<Quiz>
            {
                new Quiz { Id = 1, Title = "C# Basics", Description = "Learn variables and loops" },
                new Quiz { Id = 2, Title = "Advanced C#", Description = "Async and LINQ" },
                new Quiz { Id = 3, Title = "Unit Testing", Description = "Introduction to xUnit" }
            };
        }

        [Theory]
        [InlineData("C#", 2)]
        [InlineData("basics", 1)]
        [InlineData("async", 1)]
        [InlineData("nonexistent", 0)]
        [InlineData("", 3)]
        public void FilterQuizzes_ShouldReturnMatchingItems(string query, int expectedCount)
        {
            // Arrange
            var quizzes = GetSampleQuizzes();
            string searchText = query.ToLower();

            // Act
            var filtered = string.IsNullOrWhiteSpace(searchText) 
                ? quizzes 
                : quizzes.Where(q => q.Title.ToLower().Contains(searchText) || q.Description.ToLower().Contains(searchText)).ToList();

            // Assert
            Assert.Equal(expectedCount, filtered.Count());
        }
    }
}
