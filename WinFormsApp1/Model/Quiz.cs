namespace WinFormsApp1.Model
{
	public class Quiz
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		public List<ProgrammingTask> Tasks { get; set; } = new List<ProgrammingTask>();
	}
}