using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Services;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ArrendamientoSoftware.Web
{
    public static class CustomConfigutarion
    {

        public static WebApplicationBuilder AddCustomBuilderConfiguration(this WebApplicationBuilder builder)
        {
            // Data Context
            builder.Services.AddDbContext<DataContext>(configuration =>
            {
                configuration.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });

            // Services
            AddServices(builder);

            // Toast
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomRight;
            });

            return builder;
        }

        public static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPropiedadesService, PropiedadesService>();
            builder.Services.AddScoped<IUsuariosService, UsuariosService>();
        }

        public static WebApplication AddCustomAppConfiguration(this WebApplication app)
        {
            app.UseNotyf();
            return app;
        }
    }
}