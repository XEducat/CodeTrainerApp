namespace CodeTrainerApp.Model
{
	public class ProgrammingTask
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string CodeTemplate { get; set; } = string.Empty;
		public List<TestCase> Tests { get; set; } = new List<TestCase>();
	}
}