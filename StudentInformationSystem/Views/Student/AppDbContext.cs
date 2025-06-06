using Microsoft.EntityFrameworkCore;

namespace StudentInformationSystem.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
		{
		}

		public DbSet<StudentClass> Students { get; set; }
	}
}
