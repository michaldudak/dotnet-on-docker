using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.WebApiCompatShim;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Framework.DependencyInjection;

namespace AspNet5OnDocker.Front
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(options =>
			{
				options.OutputFormatters.Insert(0, new HttpResponseMessageOutputFormatter());
				
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.Use(async (ctx, next) =>
			{
				Console.WriteLine($"{DateTime.Now}: {ctx.Request.Method} {ctx.Request.Path.Value}");
				await next();
			});

			app.UseErrorPage();
			app.UseMvc();

			app.UseFileServer(new FileServerOptions
			{
				EnableDirectoryBrowsing = false
			});
		}
	}
}
