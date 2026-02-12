using Microsoft.AspNetCore.Identity;
using System;

namespace CodeTrainerAPI.Data
{
	public class ApplicationUser : IdentityUser
	{
		// Логін користувача
		public string Login { get; set; }

		// Дата народження
		public DateTime BirthDate { get; set; }
	}
}
