using System;
using System.Linq;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.WebApiCompatShim;
using Microsoft.AspNet.Server.Kestrel;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

namespace AspNet5OnDocker.Front
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			var configBuilder = new ConfigurationBuilder();
			configBuilder.AddEnvironmentVariables();

			IConfiguration config = configBuilder.Build();

			services.AddSingleton<IConfiguration>(s => config);

			services.AddMvc(options =>
			{
				options.OutputFormatters.Insert(0, new HttpResponseMessageOutputFormatter());
			});
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
