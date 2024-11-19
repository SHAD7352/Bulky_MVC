using BulkyWebRazor_Temp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace BulkyWebRazor_Temp.Data
{
	public class ApplicationDbContext : DbContext 
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options) : base(Options)
        {
            
        }

        public DbSet<Category> categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { id = 1, Name ="Mango", CategoryOrder = 1},
				new Category { id=2, Name="Apple", CategoryOrder = 2});
		}
		
	}
}
