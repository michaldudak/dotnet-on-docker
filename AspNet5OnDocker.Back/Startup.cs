using System;
using System.IO;
using DotNetOnDocker.Model;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;

namespace AspNet5OnDocker.Back
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddEntityFramework().AddInMemoryDatabase().AddDbContext<MyContext>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.Use(async (ctx, next) =>
			{
				Console.WriteLine($"{DateTime.Now}: {ctx.Request.Method} {ctx.Request.Path.Value}");
				try
				{
					await next();
				}

				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					throw;
				}
			});

			// Configure the HTTP request pipeline.
			app.UseStaticFiles();

			// Add MVC to the request pipeline.
			app.UseMvc();
		}
	}
}
