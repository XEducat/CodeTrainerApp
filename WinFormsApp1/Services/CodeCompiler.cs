using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.Services
{
	public static class CodeCompiler
	{
		// Клас для передачі глобальних змінних (результат виконання)
		public class Globals
		{
			public object Result { get; set; }
		}

		public static async Task<CodeRunResult> RunCode(ProgrammingTask task, string userCode)
		{
			var result = new CodeRunResult();
			result.success = true;

			string fullOutput = "";
			var options = ScriptOptions.Default
				.AddReferences(typeof(object).Assembly, typeof(ProgrammingTask).Assembly, typeof(Enumerable).Assembly)
				.AddImports("System", "System.Collections.Generic", "System.Linq", "System.Text");

			// 1. ПЕРЕВІРКА ОСНОВНОГО КОДУ (Шаблону/рішення)
			try
			{
				var userScript = CSharpScript.Create(userCode, options: options);
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

			foreach (var testCase in task.Tests)
			{
				string testMethodCall = testCase.Call.Trim();
				// Видаляємо крапку з комою в кінці, якщо вона є, щоб Roslyn міг повернути значення
				string callExpression = testMethodCall.EndsWith(";") ? testMethodCall.Substring(0, testMethodCall.Length - 1) : testMethodCall;
				string expectedValue = testCase.Expected;

				// 2. Формування коду для виконання ТЕСТУ
				// Ми НЕ використовуємо 'return', щоб підтримати void методи (вони повернуть null)
				string scriptSource = $@"
{userCode}
{callExpression}";

				try
				{
					var script = CSharpScript.Create<object>(scriptSource, options: options);
					
					// Додаємо таймаут (наприклад, 5 секунд) для захисту від нескінченних циклів
					using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5)))
					{
						var scriptTask = script.RunAsync(cancellationToken: cts.Token);
						var completedTask = await Task.WhenAny(scriptTask, Task.Delay(5500)); // Трохи більше ніж CTS для надійності

						if (completedTask != scriptTask)
						{
							result.errorMessage = "❌ ПОМИЛКА: Перевищено час виконання (можливо, нескінченний цикл або занадто складні обчислення).";
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
				}
				catch (CompilationErrorException e)
				{
					// Помилка в самому виклику тесту (Call)
					result.errorMessage = $"❌ ПОМИЛКА У ВИКЛИКУ ТЕСТУ ({testMethodCall}):\r\n" + e.Message;
					result.success = false;
					return result;
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
}