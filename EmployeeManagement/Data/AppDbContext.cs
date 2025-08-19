using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;

namespace EmployeeManagement.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Employee> Employees => Set<Employee>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employee>(entity =>
			{
				entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
				entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
				entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
				entity.HasIndex(e => e.Email).IsUnique();
			});
		}
	}
}

