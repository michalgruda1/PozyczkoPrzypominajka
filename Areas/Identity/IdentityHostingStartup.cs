using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PozyczkoPrzypominajkaV2.Areas.Identity.IdentityHostingStartup))]
namespace PozyczkoPrzypominajkaV2.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) =>
			{
			});
		}
	}
}