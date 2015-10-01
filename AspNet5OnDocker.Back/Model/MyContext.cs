using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
