using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Application.Repositories.RentalScreen;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CyberGameTime.Application;

public static class CyberGameTimeApplicationDependecyIngection
{
    public static IServiceCollection CyberGameTimelApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IRentalScreenRepository, RentalScreenRepository>();

        var connection = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<CyberGameContext>(options =>
        options.UseSqlServer(connection));

        services.AddDbContext<CyberGameContext>(options =>

             options.UseSqlServer(
                 connection,
                 sqlOptions => sqlOptions.MigrationsAssembly("CyberGameTime.Application"))
             );
        return services;
    }

}
