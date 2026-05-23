using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using Xunit;

namespace CodeTrainer.Tests.App
{
    public class CompilerTests
    {
        [Fact]
        public async Task RunCode_WithValidCode_ShouldPassTests()
        {
            // Arrange
            var task = new ProgrammingTask
            {
                Title = "Sum Test",
                CodeTemplate = "public class Solution { public int Sum(int a, int b) { return a + b; } }",
                Tests = new List<TestCase>
                {
                    new TestCase { Call = "Sum(2, 2)", Expected = "4" },
                    new TestCase { Call = "Sum(-1, 1)", Expected = "0" }
                }
            };
            string userCode = task.CodeTemplate;

            // Act
            var result = await CodeCompiler.RunCode(task, userCode);

            // Assert
            Assert.True(result.success, result.errorMessage);
            Assert.Contains("✅ Тест пройдено: Sum(2, 2) == 4", result.output);
            Assert.Contains("✅ Тест пройдено: Sum(-1, 1) == 0", result.output);
        }

        [Fact]
        public async Task RunCode_WithSyntaxError_ShouldReturnCompilationError()
        {
            // Arrange
            var task = new ProgrammingTask
            {
                Title = "Error Test",
                Tests = new List<TestCase> { new TestCase { Call = "Sum(1,1)", Expected = "2" } }
            };
            string userCode = "public class Solution { public int Sum(int a, int b) { return a + b // missing semicolon } }";

            // Act
            var result = await CodeCompiler.RunCode(task, userCode);

            // Assert
            Assert.False(result.success);
            Assert.False(result.compilationSuccess);
            Assert.Contains("ПОМИЛКА КОМПІЛЯЦІЇ ШАБЛОНУ", result.errorMessage);
        }

        [Fact]
        public async Task RunCode_WithForbiddenNamespace_ShouldReturnSecurityError()
        {
            // Arrange
            var task = new ProgrammingTask
            {
                Title = "Security Test",
                Tests = new List<TestCase> { new TestCase { Call = "Hack()", Expected = "null" } }
            };
            string userCode = "using System.IO; public class Solution { public void Hack() { File.Delete(\"test.txt\"); } }";

            // Act
            var result = await CodeCompiler.RunCode(task, userCode);

            // Assert
            Assert.False(result.success);
            Assert.Contains("ПОРУШЕННЯ БЕЗПЕКИ", result.errorMessage);
            Assert.Contains("Використання 'IO' заборонено", result.errorMessage);
        }

        [Fact]
        public async Task RunCode_WithWrongLogic_ShouldFailTests()
        {
            // Arrange
            var task = new ProgrammingTask
            {
                Title = "Wrong Logic",
                Tests = new List<TestCase> { new TestCase { Call = "Sum(1, 1)", Expected = "2" } }
            };
            string userCode = "public class Solution { public int Sum(int a, int b) { return a - b; } }"; // Should be a + b

            // Act
            var result = await CodeCompiler.RunCode(task, userCode);

            // Assert
            Assert.False(result.success);
            Assert.Contains("❌ ПОМИЛКА ТЕСТУ: Sum(1, 1)", result.output);
            Assert.Contains("Очікувалося: 2", result.output);
            Assert.Contains("Отримано:    0", result.output);
        }

        [Fact]
        public async Task RunCode_WithInfiniteLoop_ShouldTimeout()
        {
            // Arrange
            var task = new ProgrammingTask
            {
                Title = "Loop Test",
                Tests = new List<TestCase> { new TestCase { Call = "Infinite()", Expected = "1" } }
            };
            string userCode = "public class Solution { public int Infinite() { while(true) {} return 1; } }";

            // Act
            var result = await CodeCompiler.RunCode(task, userCode);

            // Assert
            Assert.False(result.success);
            // Check for presence of "час" and "виконання" which are present in both timeout scenarios
            Assert.Contains("час", result.errorMessage);
            Assert.Contains("виконання", result.errorMessage);
        }

        [Fact]
        public async Task RunCode_WithEmptyCodeAndTest_ShouldReturnCompilationError()
        {
            // Arrange
            var task = new ProgrammingTask 
            { 
                Title = "Empty",
                Tests = new List<TestCase> { new TestCase { Call = "Sum(1,1)", Expected = "2" } }
            };

            // Act
            var result = await CodeCompiler.RunCode(task, "");

            // Assert
            Assert.False(result.success);
            Assert.True(!string.IsNullOrEmpty(result.errorMessage));
        }

        [Fact]
        public async Task RunCode_WithoutClassSolution_ShouldReturnError()
        {
            // Arrange
            var task = new ProgrammingTask { Title = "No Class", Tests = new List<TestCase> { new TestCase { Call = "Test()", Expected = "1" } } };
            string userCode = "public int Test() { return 1; }"; // Method outside class

            // Act
            var result = await CodeCompiler.RunCode(task, userCode);

            // Assert
            Assert.False(result.success);
            // It might fail at template compilation or test compilation
            Assert.True(!string.IsNullOrEmpty(result.errorMessage));
        }
    }
}
