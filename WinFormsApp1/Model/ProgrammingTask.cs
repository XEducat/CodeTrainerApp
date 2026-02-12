using System.Collections.Generic;

namespace CodeTrainerApp.Model
{
	public class ProgrammingTask
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string CodeTemplate { get; set; } = string.Empty;

		// Для WinForms можна не зберігати посилання на Quiz
		// public int QuizId { get; set; }
		// public Quiz? Quiz { get; set; }

		public List<TestCase> Tests { get; set; } = new List<TestCase>();

		/// <summary>
		/// Метод для створення тестових завдань
		/// </summary>
		public static List<ProgrammingTask> GetTasks()
		{
			return new List<ProgrammingTask>
			{
				new ProgrammingTask
				{
					Id = 1,
					Title = "Метод, що повертає суму двох чисел",
					Description = "Реалізуйте метод 'Sum', який приймає два цілих числа (a, b) і повертає їхню суму.",
					CodeTemplate = @"public class Solution
{
    public int Sum(int a, int b)
    {
        // ВАШ КОД ТУТ
    }
}",
					// !!! ВИКОРИСТОВУЄМО СПИСОК ТЕСТІВ !!!
					Tests = new List<TestCase>
					{
						new TestCase { Call = "new Solution().Sum(5, 10)", Expected = "15" },
						new TestCase { Call = "new Solution().Sum(-5, 5)", Expected = "0" }, // Тест на нуль
						new TestCase { Call = "new Solution().Sum(0, 100)", Expected = "100" } // Тест з нулем
					}
				},
                // ... інше завдання (завдання №2) ...
                new ProgrammingTask
				{
					Id = 2,
					Title = "Метод, що вітає користувача",
					Description = "Реалізуйте метод 'Greet', який повертає рядок 'Привіт, {ім'я}!', використовуючи надане ім'я.",
					CodeTemplate = @"public class Solution
{
    public string Greet(string name)
    {
        // ВАШ КОД ТУТ
    }
}",
					Tests = new List<TestCase>
					{
						new TestCase { Call = "new Solution().Greet(\"Світ\")", Expected = "Привіт, Світ!" },
						new TestCase { Call = "new Solution().Greet(\"Андрій\")", Expected = "Привіт, Андрій!" }
					}
				},
new ProgrammingTask // --- ЗАВДАННЯ №3: ДОБУТОК ---
				{
					Id = 3,
					Title = "Метод, що повертає добуток чисел",
					Description = "Реалізуйте метод 'Multiply', який приймає два цілих числа (a, b) і повертає їхній добуток.",
					CodeTemplate = @"public class Solution
{
    public int Multiply(int a, int b)
    {
        // ВАШ КОД ТУТ
    }
}",
					Tests = new List<TestCase>
					{
						new TestCase { Call = "new Solution().Multiply(8, 6)", Expected = "48" },
						new TestCase { Call = "new Solution().Multiply(10, 0)", Expected = "0" },
						new TestCase { Call = "new Solution().Multiply(-5, 5)", Expected = "-25" }
					}
				},
				new ProgrammingTask // --- ЗАВДАННЯ №4: ПАРНІСТЬ ---
				{
					Id = 4,
					Title = "Перевірка числа на парність",
					Description = "Реалізуйте метод 'IsEven', який приймає ціле число (number) і повертає 'true', якщо воно парне, і 'false', якщо непарне.",
					CodeTemplate = @"public class Solution
{
    public bool IsEven(int number)
    {
        // ВАШ КОД ТУТ
    }
}",
					Tests = new List<TestCase>
					{
						new TestCase { Call = "new Solution().IsEven(7)", Expected = "False" },
						new TestCase { Call = "new Solution().IsEven(12)", Expected = "True" },
						new TestCase { Call = "new Solution().IsEven(0)", Expected = "True" } // 0 парне
					}
				},
				new ProgrammingTask // --- ЗАВДАННЯ №5: МАКСИМУМ З ТРЬОХ ---
				{
					Id = 5,
					Title = "Пошук максимуму з трьох",
					Description = "Реалізуйте метод 'MaxOfThree', який приймає три цілих числа (a, b, c) і повертає найбільше з них.",
					CodeTemplate = @"public class Solution
{
    public int MaxOfThree(int a, int b, int c)
    {
        // ВАШ КОД ТУТ
    }
}",
					Tests = new List<TestCase>
					{
						new TestCase { Call = "new Solution().MaxOfThree(10, 20, 5)", Expected = "20" },
						new TestCase { Call = "new Solution().MaxOfThree(50, 10, 50)", Expected = "50" },
						new TestCase { Call = "new Solution().MaxOfThree(-1, -5, -3)", Expected = "-1" }
					}
				},
				new ProgrammingTask // --- ЗАВДАННЯ №6: ФАКТОРІАЛ (long) ---
				{
					Id = 6,
					Title = "Обчислення факторіалу",
					Description = "Реалізуйте метод 'Factorial', який приймає ціле число N і повертає його факторіал (N!). (Пам'ятайте, 0! = 1).",
					CodeTemplate = @"public class Solution
{
    public long Factorial(int n)
    {
        // ВАШ КОД ТУТ
    }
}",
					Tests = new List<TestCase>
					{
						new TestCase { Call = "new Solution().Factorial(5)", Expected = "120" },
						new TestCase { Call = "new Solution().Factorial(0)", Expected = "1" },
						new TestCase { Call = "new Solution().Factorial(1)", Expected = "1" }
					}
				},
				new ProgrammingTask // --- ЗАВДАННЯ №7: ПРОСТЕ ЧИСЛО (ПРАЙМ) ---
				{
					Id = 7,
					Title = "Перевірка на просте число",
					Description = "Реалізуйте метод 'IsPrime', який повертає 'true', якщо задане ціле число є простим (ділиться лише на 1 і на себе).",
					CodeTemplate = @"public class Solution
{
    public bool IsPrime(int number)
    {
        // ВАШ КОД ТУТ
    }
}",
					Tests = new List<TestCase>
					{
						new TestCase { Call = "new Solution().IsPrime(17)", Expected = "True" },
						new TestCase { Call = "new Solution().IsPrime(1)", Expected = "False" }, // 1 не є простим
						new TestCase { Call = "new Solution().IsPrime(10)", Expected = "False" },
						new TestCase { Call = "new Solution().IsPrime(2)", Expected = "True" } // 2 є простим
					}
				}
			};
		}
	}
}