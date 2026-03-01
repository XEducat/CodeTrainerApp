namespace CodeTrainerApp.Model
{
	public class QuizAttempt
	{
		public int Id { get; set; }               // Id запису
		public string UserId { get; set; }        // Id користувача
		public int QuizId { get; set; }           // Id тесту
		public string QuizTitle { get; set; }     // Назва тесту
		public int Score { get; set; }            // Результат
		public DateTime Date { get; set; }        // Дата проходження
	}
}