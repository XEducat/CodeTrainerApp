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
			UserRole role)
		{
			var user = new ApplicationUser
			{
				UserName = email,
				Email = email
			};

			var result = await _userManager.CreateAsync(user, password);
			if (!result.Succeeded)
				return BadRequest(result.Errors);

			await _userManager.AddToRoleAsync(user, role.ToString());

			return Ok("User registered");
		}

		// ================= LOGIN =================
		[HttpPost("login")]
		public async Task<IActionResult> Login(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				return Unauthorized();

			var result = await _signInManager.PasswordSignInAsync(
				user,
				password,
				isPersistent: true,
				lockoutOnFailure: false);

			if (!result.Succeeded)
				return Unauthorized();

			return Ok("Logged in");
		}

		// ================= LOGOUT =================
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok("Logged out");
		}

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
