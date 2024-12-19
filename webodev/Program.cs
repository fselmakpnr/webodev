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
                        services.AddDistributedMemoryCache();  // Bellek içi cache kullanýlacak
                        services.AddSession(options =>
                        {
                            options.IdleTimeout = TimeSpan.FromMinutes(30); // Session süresi
                            options.Cookie.HttpOnly = true;  // Cookie'yi sadece HTTP istekleri için eriþilebilir yap
                            options.Cookie.IsEssential = true;  // Cookie'nin temel olmasý gerektiðini belirt
                        });

                        services.AddControllersWithViews();  // MVC ile controller ve view ekleniyor
                    });



                    webBuilder.Configure(app =>
                    {
                        // Statik dosyalarýn sunulmasý (CSS, JS, vb.)
                        app.UseStaticFiles();

                        // Session middleware'ini ekleyin
                        app.UseSession();

                        // Routing
                        app.UseRouting();

                        // Authorization middleware'ini ekleyin
                        app.UseAuthorization();

                        // Endpoint yapýlandýrmasý
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