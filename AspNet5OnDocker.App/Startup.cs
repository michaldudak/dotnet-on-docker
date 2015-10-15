using System;
using System.Linq;
using DotNetOnDocker.Model;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.WebApiCompatShim;
using Microsoft.AspNet.Server.Kestrel;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Framework.DependencyInjection;

namespace AspNet5OnDocker.App
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(options =>
			{
				options.OutputFormatters.Insert(0, new HttpResponseMessageOutputFormatter());
			});
			services.AddEntityFramework().AddInMemoryDatabase().AddDbContext<MyContext>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.Use(async (ctx, next) =>
			{
				await next();
				Console.WriteLine($"{DateTime.Now}: {ctx.Request.Method} {ctx.Request.Path.Value}: {ctx.Response.StatusCode}");
			});

			app.UseFileServer(new FileServerOptions
			{
				EnableDirectoryBrowsing = false
			});
			
			app.UseErrorPage();
			app.UseMvc();

			Console.WriteLine($"Listening on port {((ServerInformation)app.Server).Addresses.First().Port}");
		}
	}
}
