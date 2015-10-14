using Microsoft.Data.Entity;

namespace DotNetOnDocker.Model
{
	public class MyContext : DbContext
	{
		public DbSet<Todo> Todos { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase();
			base.OnConfiguring(optionsBuilder);
		}
	}
}
