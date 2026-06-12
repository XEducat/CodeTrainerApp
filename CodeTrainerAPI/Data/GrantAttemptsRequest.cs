namespace CodeTrainerAPI.Controllers
{
	public class GrantAttemptsRequest
	{
		public string UserEmail { get; set; }
		public int QuizId { get; set; }
		public int Count { get; set; }
	}
}
