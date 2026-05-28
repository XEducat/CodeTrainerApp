namespace CodeTrainerApp.Model
{
	public class UserHistory
	{
		public int Id { get; set; }               // Id запису
		public string UserId { get; set; }        // Id користувача
		public int QuizId { get; set; }           // Id тесту
		public string QuizTitle { get; set; }     // Назва тесту
		public int MaxScore { get; set; }         // Максимальний можливий результат для цього тесту
		public int Score { get; set; }            // Результат  
		public string? UserEmail { get; set; }    // Email користувача (для ментора)
		public string? UserAnswersJson { get; set; } // Відповіді користувача
		public DateTime CompletedAt { get; set; } // Дата проходження
	}
}
