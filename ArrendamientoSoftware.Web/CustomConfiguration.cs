using ArrendamientoSoftware.Web.Data;
using Microsoft.EntityFrameworkCore;

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
    }

}