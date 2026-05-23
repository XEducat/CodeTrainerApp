using CodeTrainerApp.Services;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using System.Linq;

namespace CodeTrainer.Tests.App
{
    public class CompilerSecurityTests
    {
        [Theory]
        [InlineData("using System.IO;")]
        [InlineData("var x = System.Diagnostics.Process.Start(\"cmd\");")]
        [InlineData("HttpClient client = new HttpClient();")]
        [InlineData("System.Reflection.Assembly.GetExecutingAssembly();")]
        [InlineData("File.WriteAllText(\"test.txt\", \"data\");")]
        [InlineData("Environment.Exit(0);")]
        public void ValidateSecurity_ShouldDetectForbiddenKeywords(string maliciousCode)
        {
            // Arrange
            var tree = CSharpSyntaxTree.ParseText($"public class Solution {{ public void Test() {{ {maliciousCode} }} }}");
            
            // Act
            // Використовуємо Reflection для доступу до приватного методу або міняємо його на internal
            // Оскільки ми додали InternalsVisibleTo, можемо зробити метод internal
            var errors = InvokeValidateSecurity(tree);

            // Assert
            Assert.NotEmpty(errors);
            Assert.Contains("заборонено правилами безпеки", errors.First());
        }

        private System.Collections.Generic.List<string> InvokeValidateSecurity(Microsoft.CodeAnalysis.SyntaxTree tree)
        {
            var method = typeof(CodeCompiler).GetMethod("ValidateSecurity", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            return (System.Collections.Generic.List<string>)method.Invoke(null, new object[] { tree });
        }
    }
}
