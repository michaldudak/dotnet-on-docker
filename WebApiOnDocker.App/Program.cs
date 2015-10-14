using System;
using Microsoft.Owin.Hosting;

namespace WebApiOnDocker.App
{
	class Program
	{
		private static void Main(string[] args)
		{
			const string baseAddress = "http://+:50005/";
			using (WebApp.Start<Startup>(baseAddress))
			{
				Console.WriteLine($"Server up and running on {baseAddress}");
				Console.ReadLine();
			}
		}
	}
}
