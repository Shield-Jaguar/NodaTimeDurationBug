using Microsoft.EntityFrameworkCore;
using NodaTimeDurationBug.Entities;

namespace NodaTimeDurationBug
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options):base(options)
		{
			
		}

		public DbSet<DurationEntity> Durations => Set<DurationEntity>();

		public DbSet<PeriodEntity> Periods => Set<PeriodEntity>();
	}
}