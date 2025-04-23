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
       
        string? connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        if (string.IsNullOrEmpty(connectionString))
            connectionString = configuration.GetConnectionString("ConnectionString");


        Console.WriteLine("Cadena de conexi�n usada: " + connectionString);

        services.AddDbContext<CyberGameContext>(options =>
        options.UseSqlServer(connectionString));

        services.AddDbContext<CyberGameContext>(options =>

             options.UseSqlServer(
                 connectionString,
                 sqlOptions => sqlOptions.MigrationsAssembly("CyberGameTime.Application"))
             );

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IRentalScreenRepository, RentalScreenRepository>();

        return services;
    }

}
