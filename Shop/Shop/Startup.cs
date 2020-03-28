using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data.Interfaces;
using Shop.Data.Repositories;

namespace Shop
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddMvc();
            services.AddScoped<ICarsRepository, CarsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            app.UseDeveloperExceptionPage(); // чтобы видеть ошибки
            app.UseStatusCodePages(); // отображать код запроса
            app.UseStaticFiles(); // отображать css, картинки и прочее
            app.UseRouting(); // Ќастроить маршрутизацию
            app.UseEndpoints( endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}"
                );
            } );
        }
    }
}