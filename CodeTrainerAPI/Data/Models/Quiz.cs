using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTrainerAPI.Data.Models
{
	public class Quiz
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		public string Description { get; set; }

		// ID ментора
		[Required]
		public string MentorId { get; set; }

		[ForeignKey(nameof(MentorId))]
		public ApplicationUser Mentor { get; set; }

		public List<ProgrammingTask> Tasks { get; set; } = new();
	}
}