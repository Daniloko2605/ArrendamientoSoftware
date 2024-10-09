using ArrendamientoSoftware.Web.Data;
using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Services;

namespace ArrendamientoSoftware.Web
{
    public static class CustomConfigutarion
    {

        public static WebApplicationBuilder AddCustomBuilderConfiguration(this WebApplicationBuilder builder)
        {
            
            builder.Services.AddDbContext<DataContext>(configuration =>
            {
                configuration.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });

            return builder;
        }

        public static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPropiedadesService, PropiedadesService>();
        }
    }

}