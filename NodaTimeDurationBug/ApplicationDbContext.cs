using Microsoft.EntityFrameworkCore;

namespace NodaTimeDurationBug
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options):base(options)
		{
			
		}
		public DbSet<Property> Properties => Set<Property>();
	}
}