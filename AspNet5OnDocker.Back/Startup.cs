using System;
using System.Linq;
using DotNetOnDocker.Model;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Server.Kestrel;
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
				await next();
				Console.WriteLine($"{DateTime.Now}: {ctx.Request.Method} {ctx.Request.Path.Value}: {ctx.Response.StatusCode}");
			});

			app.UseMvc();

			Console.WriteLine($"Listening on port {((ServerInformation)app.Server).Addresses.First().Port}");
		}
	}
}
