using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeTrainerAPI.Data;
using System.Security.Claims;

namespace CodeTrainerAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class UserHistoryController : ControllerBase
	{
		private readonly AppDbContext _context;

		public UserHistoryController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet("my")]
		public IActionResult GetMyHistory()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userId))
				return Unauthorized();

			var history = _context.QuizAttempts
				.Where(x => x.UserId == userId)
				.OrderByDescending(x => x.Date)
				.ToList();

			return Ok(history);
		}
	}
}