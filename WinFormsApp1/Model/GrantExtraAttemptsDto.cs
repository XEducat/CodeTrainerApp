namespace CodeTrainerApp.Model
{
    public class GrantExtraAttemptsDto
    {
        public string UserEmail { get; set; }
        public int QuizId { get; set; }
        public int Count { get; set; }
    }
}
