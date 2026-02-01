using CodeTrainerAPI.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeTrainerAPI.Data
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options) { }

		public DbSet<Quiz> Quizzes { get; set; }
		public DbSet<ProgrammingTask> ProgrammingTasks { get; set; }
		public DbSet<TestCase> TestCases { get; set; }
	}
}
