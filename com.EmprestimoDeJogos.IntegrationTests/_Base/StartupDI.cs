using com.EmprestimoDeJogos.Core.Interfaces;
using com.EmprestimoDeJogos.Core.Services;
using com.EmprestimoDeJogos.Infrastructure;
using com.EmprestimoDeJogos.Infrastructure.Data;
using com.EmprestimoDeJogos.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.EmprestimoDeJogos.IntegrationTests._Base
{
    public class StartupDI
    {
        public IConfigurationRoot Configuration { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        public StartupDI()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = configBuilder.Build();
            var services = new ServiceCollection();
            services.AddDbContext<LoanGameContext>(c =>
                    c.UseSqlServer(Configuration.GetConnectionString("LoanGameConnection")));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<ITokenClaimsService, IdentityTokenClaimService>();


            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
