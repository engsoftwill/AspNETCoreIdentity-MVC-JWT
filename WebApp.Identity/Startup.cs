using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WebApp.Identity
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
            services.AddControllersWithViews();


            var connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=IdentityCurso;Data Source=DESKTOP-F99HABO\SQLEXPRESS01";
            var migratrionAssembly = typeof(Startup)
                .GetTypeInfo().Assembly
                .GetName().Name;
            services.AddDbContext<MyUserDbContext>(
                options => options.UseSqlServer(connectionString,
                sql => sql.MigrationsAssembly(migratrionAssembly))
                );


            services.AddIdentity<MyUser, IdentityRole>(options => { })
                .AddEntityFrameworkStores<MyUserDbContext>();
            
            services.AddScoped<IUserStore<MyUser>,
                UserOnlyStore<MyUser, MyUserDbContext>>();
            
            services.AddScoped<IUserClaimsPrincipalFactory<MyUser>,
                MyUserClaimsPrincipalFactory>();
        
            services.ConfigureApplicationCookie(options =>
                options.LoginPath = "/Home/Login"
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
