using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace webodev
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        // Session için gerekli yapýlandýrmalar
                        services.AddDistributedMemoryCache();  
                        services.AddSession(options =>
                        {
                            options.IdleTimeout = TimeSpan.FromMinutes(30); 
                            options.Cookie.HttpOnly = true;  
                            options.Cookie.IsEssential = true;  
                        });

                        services.AddControllersWithViews();  
                    });



                    webBuilder.Configure(app =>
                    {
                        
                        app.UseStaticFiles();

                       
                        app.UseSession();

                       
                        app.UseRouting();

                       
                        app.UseAuthorization();

                        
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}");
                        });
                    });
                });
    }
}