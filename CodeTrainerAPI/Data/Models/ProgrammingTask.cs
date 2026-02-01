using System.ComponentModel.DataAnnotations;

namespace CodeTrainerAPI.Data.Models
{
	public class ProgrammingTask
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		public string Description { get; set; }

		public string CodeTemplate { get; set; }

		// Зв'язок з Quiz
		public int QuizId { get; set; }
		public List<TestCase> Tests { get; set; } = new();
	}
}
