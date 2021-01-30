using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shega_meetups_api.Data;
using shega_meetups_api.Interfaces;
using shega_meetups_api.Services;

namespace shega_meetups_api.Extension
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config )
        {
             services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DbConnection"));
            });

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}