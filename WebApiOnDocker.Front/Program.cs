using System;
using Microsoft.Owin.Hosting;

namespace WebApiOnDocker.Front
{
	class Program
	{
		private static void Main(string[] args)
		{
			const string baseAddress = "http://+:9000/";
			using (WebApp.Start<Startup>(baseAddress))
			{
				Console.WriteLine($"Server up and running on {baseAddress}");
				Console.ReadLine();
			}
		}
	}
}
