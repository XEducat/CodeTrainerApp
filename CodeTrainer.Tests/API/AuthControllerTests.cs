using CodeTrainerAPI.Controllers;
using CodeTrainerAPI.Data.Models;
using CodeTrainerAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CodeTrainer.Tests.API
{
    public class AuthControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            // Mocking UserManager
            var store = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            // Mocking SignInManager
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                _mockUserManager.Object, contextAccessor.Object, claimsFactory.Object, null, null, null, null);

            _controller = new AuthController(_mockUserManager.Object, _mockSignInManager.Object);
        }

        [Fact]
        public async Task Register_WithValidData_ShouldReturnOk()
        {
            // Arrange
            string email = "test@example.com";
            string password = "Password123!";
            string name = "TestUser";
            DateTime birthDate = DateTime.Now.AddYears(-20);

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(email, password, name, birthDate, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("User registered", okResult.Value.ToString());
        }

        [Fact]
        public async Task Register_MentorWithWrongCode_ShouldReturnBadRequest()
        {
            // Arrange
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register("m@m.com", "pass", "M", DateTime.Now, "WRONG-CODE");

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Невірний код ментора", badResult.Value);
        }

        [Fact]
        public async Task Register_MentorWithCorrectCode_ShouldAssignMentorRole()
        {
            // Arrange
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "Mentor"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register("m@m.com", "pass", "M", DateTime.Now, "MENTOR-2026");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("role = Mentor", okResult.Value.ToString());
        }

        [Fact]
        public async Task Login_WithValidCredentials_ShouldReturnLoginDto()
        {
            // Arrange
            var user = new ApplicationUser { Email = "u@u.com", Login = "User1" };
            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            _mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string> { "Student" });

            // Act
            var result = await _controller.Login("u@u.com", "pass");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<LoginDto>(okResult.Value);
            Assert.Equal("User1", response.Login);
            Assert.Equal("Student", response.Role);
        }
    }
}
