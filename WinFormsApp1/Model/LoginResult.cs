namespace WinFormsApp1.Model
{
	// клас для розбору відповіді API при логіні
	public class LoginResult
	{
		public string Token { get; set; }
		public string Username { get; set; }
		public string Role { get; set; }
	}
}
