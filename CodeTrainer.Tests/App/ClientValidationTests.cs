using CodeTrainerApp.Services;
using Xunit;

namespace CodeTrainer.Tests.App
{
    public class ClientValidationTests
    {
        [Theory]
        [InlineData("test@example.com", true)]
        [InlineData("user.name@domain.co.uk", true)]
        [InlineData("invalid-email", false)]
        [InlineData("missing@domain", false)]
        [InlineData("@no-user.com", false)]
        [InlineData("space in@email.com", false)]
        public void IsValidEmail_ShouldValidateCorrectly(string email, bool expected)
        {
            // Arrange
            var userService = UserService.Instance;

            // Act
            bool result = userService.IsValidEmail(email);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
