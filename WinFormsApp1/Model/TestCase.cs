namespace WinFormsApp1.Model
{
	public struct TestCase
	{
		public int Id { get; set; }
		public string Call { get; set; }        // Код C#, який викликає метод користувача.
		public string Expected { get; set; }    // Очікуваний результат цього виклику.
	}
}
