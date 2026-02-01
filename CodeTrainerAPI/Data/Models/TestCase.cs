using System.ComponentModel.DataAnnotations;

namespace CodeTrainerAPI.Data.Models
{
	public class TestCase
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Call { get; set; }

		[Required]
		public string Expected { get; set; }

		// Зв'язок з ProgrammingTask
		public int ProgrammingTaskId { get; set; }
	}
}
