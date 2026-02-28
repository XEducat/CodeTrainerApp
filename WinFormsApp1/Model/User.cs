namespace CodeTrainerApp.Model
{
	public class User
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
		public DateTime BirthDate { get; set; }
		public string Role { get; set; }

		public User(string id, string email, string login, DateTime birthDate, string role)
		{
			Id = id;
			Email = email;
			Login = login;
			BirthDate = birthDate;
			Role = role;
		}
	}
}