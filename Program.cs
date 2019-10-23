using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace PozyczkoPrzypominajkaV2
{
	public class Program
	{
		IConfiguration config { get; }

		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.UseStartup<Startup>()
			.ConfigureKestrel((context, options) =>
			{
				var ip = context.Configuration.GetSection("Host").GetSection("Ip").Value;
				var port = context.Configuration.GetSection("Host").GetSection("Port").Value;
				options.Listen(IPAddress.Parse(ip), int.Parse(port));
			});
	}
}
