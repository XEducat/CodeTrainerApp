namespace CodeTrainerAPI.DTO
{
	public class UserHistoryDto
	{
		public int Id { get; set; }
		public int QuizId { get; set; }
		public string QuizTitle { get; set; }
		public int MaxScore { get; set; }
		public int Score { get; set; }
		public string? UserEmail { get; set; }
		public string? UserName { get; set; }
		public string? UserAnswersJson { get; set; }
		public DateTime CompletedAt { get; set; }
	}
}