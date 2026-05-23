using CodeTrainerApp.Services;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace CodeTrainer.Tests.App
{
    public class CompilerInstrumentationTests
    {
        [Fact]
        public void InstrumentCode_ShouldInjectCheckIntoWhileLoop()
        {
            // Arrange
            string originalCode = "public class Solution { public void Loop() { while(true) { int i = 0; } } }";
            var tree = CSharpSyntaxTree.ParseText(originalCode);

            // Act
            string instrumented = InvokeInstrumentCode(tree);

            // Assert
            Assert.Contains("CodeTrainerApp.Services.CodeCompiler.CurrentToken.ThrowIfCancellationRequested()", instrumented);
        }

        [Fact]
        public void InstrumentCode_ShouldInjectCheckIntoForLoop()
        {
            // Arrange
            string originalCode = "public class Solution { public void Loop() { for(int i=0; i<10; i++) { } } }";
            var tree = CSharpSyntaxTree.ParseText(originalCode);

            // Act
            string instrumented = InvokeInstrumentCode(tree);

            // Assert
            Assert.Contains("CodeTrainerApp.Services.CodeCompiler.CurrentToken.ThrowIfCancellationRequested()", instrumented);
        }

        private string InvokeInstrumentCode(Microsoft.CodeAnalysis.SyntaxTree tree)
        {
            var method = typeof(CodeCompiler).GetMethod("InstrumentCode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            return (string)method.Invoke(null, new object[] { tree });
        }
    }
}
