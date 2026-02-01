using System.ComponentModel.DataAnnotations;

namespace CodeTrainerAPI.Data.Models
{
	public class Quiz
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; } // Назва паку тестів

		public string Description { get; set; }

		public List<ProgrammingTask> Tasks { get; set; } = new();
	}
}
