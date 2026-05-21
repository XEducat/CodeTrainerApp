using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.Services
{
	public static class CodeCompiler
	{
		// Статичне поле для доступу з інструментованого коду
		public static System.Threading.CancellationToken CurrentToken;

		/// <summary>
		/// Перевіряє код на наявність небезпечних операцій (IO, мережа, системні ресурси).
		/// </summary>
		private static List<string> ValidateSecurity(SyntaxTree tree)
		{
			var errors = new List<string>();
			try
			{
				var root = tree.GetRoot();

				// Список заблокованих ключових слів/типів
				var forbidden = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
					"IO", "File", "Directory", "Path", "Stream", "FileSystem", "DriveInfo",
					"Net", "HttpClient", "WebClient", "Socket", "WebRequest", "TcpClient", "UdpClient",
					"Diagnostics", "Process", "Registry", "AppDomain", "Assembly", "Reflection",
					"InteropServices", "DllImport", "Environment", "Thread", "Task", "Web"
				};

				var nodes = root.DescendantNodes();
				foreach (var node in nodes)
				{
					string found = null;
					if (node is IdentifierNameSyntax id && forbidden.Contains(id.Identifier.Text))
						found = id.Identifier.Text;
					else if (node is GenericNameSyntax gn && forbidden.Contains(gn.Identifier.Text))
						found = gn.Identifier.Text;
					else if (node is UsingDirectiveSyntax uds)
					{
						string name = uds.Name.ToString();
						var part = name.Split('.').FirstOrDefault(p => forbidden.Contains(p));
						if (part != null) found = part;
					}

					if (found != null)
						errors.Add($"❌ Використання '{found}' заборонено правилами безпеки.");
				}
			}
			catch { }
			return errors.Distinct().ToList();
		}

		/// <summary>
		/// Інструментує код користувача, додаючи перевірки на скасування у цикли та методи.
		/// </summary>
		private static string InstrumentCode(SyntaxTree tree)
		{
			try
			{
				var root = tree.GetRoot();
				var rewriter = new LoopRewriter();
				var newRoot = rewriter.Visit(root);
				return newRoot.ToFullString();
			}
			catch
			{
				return tree.ToString();
			}
		}

		public static async Task<CodeRunResult> RunCode(ProgrammingTask task, string userCode)
		{
			var result = new CodeRunResult();
			result.success = true;

			// Парсимо код один раз для аналізу та інструментації
			var tree = CSharpSyntaxTree.ParseText(userCode);

			// 1. ПЕРЕВІРКА БЕЗПЕКИ
			var securityErrors = ValidateSecurity(tree);
			if (securityErrors.Any())
			{
				result.errorMessage = "❌ ПОРУШЕННЯ БЕЗПЕКИ:\r\n" + string.Join("\r\n", securityErrors);
				result.success = false;
				return result;
			}

			// 2. ІНСТРУМЕНТАЦІЯ (Захист від нескінченних циклів)
			string instrumentedUserCode = InstrumentCode(tree);

			string fullOutput = "";
			var options = ScriptOptions.Default
				.AddReferences(typeof(object).Assembly, typeof(ProgrammingTask).Assembly, typeof(Enumerable).Assembly, typeof(CodeCompiler).Assembly)
				.AddImports("System", "System.Collections.Generic", "System.Linq", "System.Text", "System.Threading");

			// 3. ПЕРЕВІРКА КОМПІЛЯЦІЇ
			try
			{
				var userScript = CSharpScript.Create(instrumentedUserCode, options: options);
				userScript.Compile();
			}
			catch (CompilationErrorException e)
			{
				var diagnostics = e.Diagnostics
					.Select(d => {
						var lineSpan = d.Location.GetLineSpan();
						int line = lineSpan.StartLinePosition.Line + 1;
						int col = lineSpan.StartLinePosition.Character + 1;
						return $"[Рядок {line}, Символ {col}] {d.GetMessage()}";
					});

				result.errorMessage = "❌ ПОМИЛКА В ОСНОВНОМУ КОДІ:\r\n" + string.Join("\r\n", diagnostics);
				result.success = false;
				return result;
			}
			catch (Exception e)
			{
				result.errorMessage = "❌ Критична помилка аналізу коду:\r\n" + e.Message;
				result.success = false;
				return result;
			}

			// 4. ВИКОНАННЯ ТЕСТІВ
			foreach (var testCase in task.Tests)
			{
				string testMethodCall = testCase.Call.Trim();
				string callExpression = testMethodCall.EndsWith(";") ? testMethodCall.Substring(0, testMethodCall.Length - 1) : testMethodCall;
				string expectedValue = testCase.Expected;

				string scriptSource = $@"
{instrumentedUserCode}
{callExpression}";

				try
				{
					var script = CSharpScript.Create<object>(scriptSource, options: options);
					
					using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5)))
					{
						CodeCompiler.CurrentToken = cts.Token;
						
						try
						{
							var scriptTask = script.RunAsync(cancellationToken: cts.Token);
							var completedTask = await Task.WhenAny(scriptTask, Task.Delay(6000));

							if (completedTask != scriptTask)
							{
								result.errorMessage = "❌ ПОМИЛКА: Перевищено час виконання (можливо, нескінченний цикл).";
								result.success = false;
								return result;
							}

							var scriptState = await scriptTask;
							object returnValue = scriptState.ReturnValue;
							string actualResult = returnValue?.ToString() ?? "null";
							
							string shortMethodCall = testMethodCall.Replace("new Solution().", "");

							if (actualResult.Equals(expectedValue, StringComparison.OrdinalIgnoreCase))
							{
								fullOutput += $"✅ Тест пройдено: {shortMethodCall} == {actualResult}\r\n";
							}
							else
							{
								result.success = false;
								fullOutput += $"❌ ПОМИЛКА ТЕСТУ: {shortMethodCall}\r\n";
								fullOutput += $"   Очікувалося: {expectedValue}\r\n";
								fullOutput += $"   Отримано:    {actualResult}\r\n\n";
							}
						}
						catch (OperationCanceledException)
						{
							result.errorMessage = "❌ ПОМИЛКА: Виконання було перервано через перевищення часу виконання коду(Timeout).";
							result.success = false;
							return result;
						}
					}
				}
				catch (Exception e)
				{
					result.errorMessage = "❌ Помилка виконання:\r\n" + e.Message;
					result.success = false;
					return result;
				}
			}

			result.output = fullOutput;
			return result;
		}
	}

	internal class LoopRewriter : CSharpSyntaxRewriter
	{
		public override SyntaxNode VisitWhileStatement(WhileStatementSyntax node)
		{
			var newNode = (WhileStatementSyntax)base.VisitWhileStatement(node);
			return newNode.WithStatement(InjectCheck(newNode.Statement));
		}

		public override SyntaxNode VisitForStatement(ForStatementSyntax node)
		{
			var newNode = (ForStatementSyntax)base.VisitForStatement(node);
			return newNode.WithStatement(InjectCheck(newNode.Statement));
		}

		public override SyntaxNode VisitDoStatement(DoStatementSyntax node)
		{
			var newNode = (DoStatementSyntax)base.VisitDoStatement(node);
			return newNode.WithStatement(InjectCheck(newNode.Statement));
		}

		public override SyntaxNode VisitForEachStatement(ForEachStatementSyntax node)
		{
			var newNode = (ForEachStatementSyntax)base.VisitForEachStatement(node);
			return newNode.WithStatement(InjectCheck(newNode.Statement));
		}

		public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
		{
			var newNode = (MethodDeclarationSyntax)base.VisitMethodDeclaration(node);
			if (newNode.Body != null)
			{
				return newNode.WithBody((BlockSyntax)InjectCheck(newNode.Body));
			}
			return newNode;
		}

		private StatementSyntax InjectCheck(StatementSyntax statement)
		{
			var checkStatement = SyntaxFactory.ParseStatement("CodeTrainerApp.Services.CodeCompiler.CurrentToken.ThrowIfCancellationRequested();\r\n");
			if (statement is BlockSyntax block)
			{
				return block.WithStatements(block.Statements.Insert(0, checkStatement));
			}
			return SyntaxFactory.Block(checkStatement, statement);
		}
	}
}