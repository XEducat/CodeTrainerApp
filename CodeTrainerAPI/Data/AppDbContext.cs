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
		public DbSet<UserHistory> UserHistories { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Quiz>()
				.HasOne(q => q.Mentor)
				.WithMany()
				.HasForeignKey(q => q.MentorId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}