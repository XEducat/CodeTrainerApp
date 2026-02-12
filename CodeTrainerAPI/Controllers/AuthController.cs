using CodeTrainerAPI.Data;
using CodeTrainerAPI.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeTrainerAPI.Controllers
{
	[ApiController]
	[Route("api/auth")]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		private const string MentorSecretCode = "MENTOR-2024";

		public AuthController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		// ================= REGISTER =================
		[HttpPost("register")]
		public async Task<IActionResult> Register(
			string email,
			string password,
			string name,
			DateTime birthDate,
			string? mentorCode)
		{
			if (string.IsNullOrWhiteSpace(email) ||
				string.IsNullOrWhiteSpace(password) ||
				string.IsNullOrWhiteSpace(name))
			{
				return BadRequest("Некоректні реєстраційні дані");
			}

			var user = new ApplicationUser
			{
				UserName = email,
				Email = email,
				Login = name,
				BirthDate = birthDate
			};

			var createResult = await _userManager.CreateAsync(user, password);
			if (!createResult.Succeeded)
				return BadRequest(createResult.Errors);

			// ===== ВИЗНАЧЕННЯ РОЛІ =====
			string role = "Student";

			if (!string.IsNullOrWhiteSpace(mentorCode))
			{
				if (mentorCode != MentorSecretCode)
				{
					await _userManager.DeleteAsync(user);
					return BadRequest("Невірний код ментора");
				}

				role = "Mentor";
			}

			await _userManager.AddToRoleAsync(user, role);

			return Ok(new
			{
				message = "User registered",
				role = role
			});
		}

		// ================= LOGIN =================
		[HttpPost("login")]
		public async Task<IActionResult> Login(string loginOrEmail, string password)
		{
			if (string.IsNullOrWhiteSpace(loginOrEmail) ||
				string.IsNullOrWhiteSpace(password))
			{
				return BadRequest("Некоректні дані для входу");
			}

			// Пошук по email
			var user = await _userManager.FindByEmailAsync(loginOrEmail);

			// Якщо не знайдено — шукаємо по Login
			if (user == null)
			{
				user = _userManager.Users
					.FirstOrDefault(u => u.Login == loginOrEmail);
			}

			if (user == null)
				return Unauthorized("Користувача не знайдено");

			// Перевірка пароля
			var result = await _signInManager.PasswordSignInAsync(
				user,
				password,
				isPersistent: true,
				lockoutOnFailure: false);

			if (!result.Succeeded)
				return Unauthorized("Невірний пароль");

			var roles = await _userManager.GetRolesAsync(user);

			return Ok(new LoginResponse
			{
				Id = user.Id,
				Email = user.Email,
				Login = user.Login,
				BirthDate = user.BirthDate,
				Role = roles.FirstOrDefault() ?? "Student"
			});
		}

		// ================= LOGOUT =================
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok("Logged out");
		}

		// ================= DELETE =================
		[HttpDelete("delete")]
		public async Task<IActionResult> DeleteUser(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				return NotFound("User not found");

			var result = await _userManager.DeleteAsync(user);
			if (!result.Succeeded)
				return BadRequest(result.Errors);

			return Ok("User deleted");
		}
	}
}
