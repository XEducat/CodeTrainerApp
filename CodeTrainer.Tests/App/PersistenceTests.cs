using CodeTrainerApp.Services;
using CodeTrainerApp.Model;
using Xunit;

namespace CodeTrainer.Tests.App
{
    public class PersistenceTests : IDisposable
    {
        private readonly string _storageDir;
        private readonly string _userFile;
        private readonly HttpClient _mockClient;

        public PersistenceTests()
        {
            _storageDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CodeTrainerApp_Test");
            _userFile = Path.Combine(_storageDir, "user.json");
            _mockClient = new HttpClient();

            if (Directory.Exists(_storageDir)) Directory.Delete(_storageDir, true);
            Directory.CreateDirectory(_storageDir);
        }

        [Fact]
        public void UserService_SaveAndLoad_ShouldRestoreUser()
        {
            // Arrange
            // We use reflection to set the private storage fields for testing
            var userService = new UserService(_mockClient);
            var type = typeof(UserService);
            type.GetField("_storageDir", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(userService, _storageDir);
            type.GetField("_userFile", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(userService, _userFile);

            var user = new User("1", "test@test.com", "tester", DateTime.Now.AddYears(-20), "Student");
            type.GetProperty("CurrentUser").SetValue(userService, user);

            // Act
            type.GetMethod("SaveCurrentUserToDisk", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(userService, null);
            
            // Create a new instance and try to load
            var newService = new UserService(_mockClient);
            type.GetField("_storageDir", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(newService, _storageDir);
            type.GetField("_userFile", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(newService, _userFile);
            
            type.GetMethod("LoadCurrentUserFromDisk", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(newService, null);

            // Assert
            Assert.NotNull(newService.CurrentUser);
            Assert.Equal("tester", newService.CurrentUser.Login);
            Assert.Equal("Student", newService.CurrentUser.Role);
        }

        public void Dispose()
        {
            if (Directory.Exists(_storageDir)) Directory.Delete(_storageDir, true);
        }
    }
}
