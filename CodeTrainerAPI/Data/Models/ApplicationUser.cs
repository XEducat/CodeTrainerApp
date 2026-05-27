using Microsoft.AspNetCore.Identity;

namespace CodeTrainerAPI.Data.Models
{
	public class ApplicationUser : IdentityUser
	{
		// Логін користувача
		public string Login { get; set; }

		// Дата народження
		public DateTime BirthDate { get; set; }
	}
}
