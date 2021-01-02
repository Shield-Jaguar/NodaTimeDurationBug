using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTimeDurationBug.Entities;

namespace NodaTimeDurationBug
{
	static class Program
	{
		static async Task Main()
		{
			await using var dbContext = new ApplicationDbContext(GetOptions());
			await dbContext.Database.EnsureDeletedAsync();
			await dbContext.Database.EnsureCreatedAsync();

			const int seconds = 2148;

			dbContext.Add(new DurationEntity
			{
				Id = Guid.NewGuid(),
				Value = Duration.FromSeconds(seconds),
			});

			dbContext.Add(new PeriodEntity
			{
				Id = Guid.NewGuid(),
				Value = Period.FromSeconds(seconds),
			});

			await dbContext.SaveChangesAsync();

			dbContext.ChangeTracker.Clear();

			foreach (var durationEntity in await dbContext.Durations.ToListAsync())
			{
				Console.WriteLine(durationEntity);
			}

			foreach (var periodEntity in await dbContext.Periods.ToListAsync())
			{
				Console.WriteLine(periodEntity);
			}
		}

		private static DbContextOptions GetOptions()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseNpgsql("Host=localhost;Database=TestDb;Username=postgres;Password=123456", o =>
			{
				o.UseNodaTime();
			});
			return optionsBuilder.Options;
		}
	}
}
