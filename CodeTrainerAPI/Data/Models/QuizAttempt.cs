using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTrainerAPI.Data.Models
{
	public class QuizAttempt
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string UserId { get; set; }

		[ForeignKey("UserId")]
		public ApplicationUser User { get; set; }

		[Required]
		public int QuizId { get; set; }

		[ForeignKey("QuizId")]
		public Quiz Quiz { get; set; }

		public int Score { get; set; }

		public int TotalQuestions { get; set; }

		public DateTime Date { get; set; } = DateTime.UtcNow;
	}
}