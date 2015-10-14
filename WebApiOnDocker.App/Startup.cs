using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace WebApiOnDocker.App
{
	class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var relativePath = string.Format(@"..{0}..{0}", Path.DirectorySeparatorChar);
			var contentPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), relativePath);

			app.UseErrorPage();

			app.Use(async (ctx, next) =>
			{
				await next();
				Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: {ctx.Request.Method} {ctx.Request.Uri.AbsolutePath}: {ctx.Response.StatusCode} {ctx.Response.ReasonPhrase}");
			});

			app.UseFileServer(new FileServerOptions()
			{
				RequestPath = PathString.Empty,
				FileSystem = new PhysicalFileSystem(Path.Combine(contentPath, @"wwwroot")),
				EnableDirectoryBrowsing = false
			});

			var config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			app.UseWebApi(config);
		}
	}
}
