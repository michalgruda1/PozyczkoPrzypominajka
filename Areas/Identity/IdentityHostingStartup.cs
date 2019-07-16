using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PozyczkoPrzypominajka.Models;
using PozyczkoPrzypominajkaV2.Data;

[assembly: HostingStartup(typeof(PozyczkoPrzypominajkaV2.Areas.Identity.IdentityHostingStartup))]
namespace PozyczkoPrzypominajkaV2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}