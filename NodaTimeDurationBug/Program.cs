using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace NodaTimeDurationBug
{
	static class Program
	{
		static async Task Main()
		{
			await using var dbContext = new ApplicationDbContext(GetOptions());
			await dbContext.Database.EnsureCreatedAsync();

			dbContext.Add(new Property
			{
				Id = Guid.NewGuid(),
				StartDuration = Duration.FromSeconds(0),
				EndDuration = Duration.FromSeconds(39600),
			});

			dbContext.Add(new Property
			{
				Id = Guid.NewGuid(),
				StartDuration = Duration.FromSeconds(3600),
				EndDuration = Duration.FromSeconds(39600),
			});

			await dbContext.SaveChangesAsync();

			dbContext.ChangeTracker.Clear();

			var allProperties = await dbContext.Properties.ToListAsync();
			foreach (var property in allProperties)
			{
				Console.WriteLine(property);
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
