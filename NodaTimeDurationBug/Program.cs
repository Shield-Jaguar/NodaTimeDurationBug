using System;
using System.Collections.Generic;
using System.Net.Mime;
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
			// await dbContext.Database.EnsureDeletedAsync();
			await dbContext.Database.EnsureCreatedAsync();

			dbContext.Add(new DurationEntity
			{
				Id = Guid.NewGuid(),
				Value = Duration.FromSeconds(3600),
			});

			dbContext.Add(new PeriodEntity
			{
				Id = Guid.NewGuid(),
				Value = Period.FromSeconds(3600),
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
