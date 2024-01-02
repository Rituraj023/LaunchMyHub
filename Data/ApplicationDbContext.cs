using LaunchMyHub.Models;
using Microsoft.EntityFrameworkCore;

namespace LaunchMyHub.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
		{
		}

		public DbSet<HubType> HubTypes { get; set; }
		public DbSet<ReferralSource> ReferralSources { get; set; }
		public DbSet<HubMaster> HubMasters { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed data for HubType
			modelBuilder.Entity<HubType>().HasData(
				new HubType { Id = 1, Name = "Type A", Description = "Description for Type A" },
				new HubType { Id = 2, Name = "Type B", Description = "Description for Type B" }
		        // Add more entries as needed
			);

			// Seed data for ReferralSource
			modelBuilder.Entity<ReferralSource>().HasData(
				new ReferralSource { Id = 1, Name = "Source A", Description = "Description for Source A" },
				new ReferralSource { Id = 2, Name = "Source B", Description = "Description for Source B" }
		        // Add more entries as needed
			);

		}
	}

}
