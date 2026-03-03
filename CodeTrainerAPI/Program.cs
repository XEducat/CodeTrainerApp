using CodeTrainerAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ================= DB =================
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ================= Identity =================
builder.Services
	.AddIdentity<ApplicationUser, IdentityRole>(options =>
	{
		options.Password.RequireDigit = false;
		options.Password.RequireLowercase = false;
		options.Password.RequireUppercase = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequiredLength = 6;
	})
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();

// ================= COOKIE CONFIG =================
builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.HttpOnly = true;
	options.Cookie.Name = "CodeTrainerAuth";

	options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
	options.SlidingExpiration = true;

	options.LoginPath = "/api/user/login";
	options.AccessDeniedPath = "/api/user/login";

	// ÂÀÆËÈÂÎ äëÿ API
	options.Events.OnRedirectToLogin = context =>
	{
		context.Response.StatusCode = 401;
		return Task.CompletedTask;
	};

	options.Events.OnRedirectToAccessDenied = context =>
	{
		context.Response.StatusCode = 403;
		return Task.CompletedTask;
	};
});

// ================= Controllers =================
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ================= MIGRATIONS + ROLES =================
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	await db.Database.MigrateAsync();

	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	string[] roles = { "Student", "Mentor" };

	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
		{
			await roleManager.CreateAsync(new IdentityRole(role));
		}
	}
}

// ================= Middleware =================
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();

app.Run();