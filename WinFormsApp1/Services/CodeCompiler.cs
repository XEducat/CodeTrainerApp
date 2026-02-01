using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using WinFormsApp1.Model;

namespace WinFormsApp1.Services
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
			result.success = true; // Припускаємо успіх на початку

			// Починаємо з порожнього виводу
			string fullOutput = "";
			// Налаштування Roslyn
			var options = ScriptOptions.Default
				.AddReferences(typeof(object).Assembly, typeof(ProgrammingTask).Assembly)
				.AddImports("System");

			foreach (var testCase in task.Tests) // !!! ІТЕРУЄМО ПО ВСІХ ТЕСТАХ !!!
			{
				string testMethodCall = testCase.Call;
				string expectedValue = testCase.Expected;

				// 1. Формування повного коду для виконання
				string scriptSource = $@"
using System;
{userCode} // Код, наданий користувачем
return {testMethodCall}; // Виклик тестового методу
";

				try
				{
					// 2. Виконання коду за допомогою Roslyn
					// (припускаємо, що у вас налаштовані опції для LINQ та List<T>)
					var runner = CSharpScript.Create(scriptSource, options: options);
					var scriptResult = await runner.RunAsync();

					string actualResult = scriptResult.ReturnValue?.ToString() ?? "null";
					string shortMethodCall = testMethodCall.Replace("new Solution().", "");

					// 3. Перевірка результату
					if (actualResult.Equals(expectedValue, StringComparison.OrdinalIgnoreCase))
					{
						// Успіх для цього тесту
						fullOutput += $"Тест успішно пройдено: {shortMethodCall} = {actualResult}\r\n";
					}
					else
					{
						// Помилка для цього тесту
						result.success = false;
						fullOutput += $"ПОМИЛКА ТЕСТУ: {shortMethodCall}\r\n";
						fullOutput += $"   Очікувалося: {expectedValue}\r\n";
						fullOutput += $"   Отримано:   {actualResult}\r\n\n";

						// Якщо хоча б один тест провалився, ми можемо перервати виконання
						// або продовжити для збору всіх помилок. Залишимо, щоб показати всі помилки.
					}
				}
				catch (CompilationErrorException e)
				{
					// Помилка компіляції (синтаксис)
					result.errorMessage = "Помилка компіляції:\r\n" + e.Message;
					result.output = fullOutput; // Додаємо те, що вже зібрали
					result.success = false;
					return result;
				}
				catch (Exception e)
				{
					// Помилка виконання (Runtime)
					result.errorMessage = "Помилка виконання:\r\n" + e.Message;
					result.output = fullOutput;
					result.success = false;
					return result;
				}
			} // Кінець циклу по тестах

			result.output = fullOutput;
			return result;
		}
	}
}