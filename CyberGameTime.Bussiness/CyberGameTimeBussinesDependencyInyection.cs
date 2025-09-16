using CyberGameTime.Bussiness.BackGroundTask;
using CyberGameTime.Bussiness.Helpers;
using CyberGameTime.Bussiness.Helpers.Conectivity.Roku;
using CyberGameTime.Bussiness.Profiles;
using CyberGameTime.Bussiness.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
namespace CyberGameTime.Bussiness;

public static class CyberGameTimeBussinesDependencyInyection
{
    public static IServiceCollection CyberGameTimeBussines(this IServiceCollection services, IConfiguration configuration)
    {
       
        services.AddAutoMapper(x => x.AddProfile(typeof(ScreenProfile)));
        services.AddAutoMapper(x => x.AddProfile(typeof(RentalScreenProfile)));

        services.AddMediatR(_config =>
        {
            _config.RegisterServicesFromAssemblies(typeof(CyberGameTimeBussinesDependencyInyection).Assembly);
        });
        
        services.AddSingleton<ILogService, LogService>();
        services.AddScoped<ISendMessageSignalR, SendMessageSignalR>();
        //services.AddHostedService<GetRokuDevicesTask>();
        //services.AddHostedService<ValidationPowerOffBAckGroundTask>();

        return services;
    }

}
