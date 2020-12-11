using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using com.EmprestimoDeJogos.Infrastructure;
using com.EmprestimoDeJogos.Core.Interfaces;
using com.EmprestimoDeJogos.Core.Services;
using com.EmprestimoDeJogos.Infrastructure.Data;

namespace com.EmprestimoDeJogos.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<LoanGameContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("LoanGameConnection")));

            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IFriendRepository, FriendRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }


        //public void ConfigureProductionServices(IServiceCollection services)
        //{
        //    // use real database
        //    // Requires LocalDB which can be installed with SQL Server Express 2016
        //    // https://www.microsoft.com/en-us/download/details.aspx?id=54284
        //    services.AddDbContext<LoanGameContext>(c =>
        //        c.UseSqlServer(Configuration.GetConnectionString("LoanGameConnection")));

        //    ConfigureServices(services);
        //}
    }
}
