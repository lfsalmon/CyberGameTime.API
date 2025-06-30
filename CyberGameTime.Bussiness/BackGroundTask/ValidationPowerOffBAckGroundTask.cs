using AutoMapper;
using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Handler.Conectivity;
using CyberGameTime.Bussiness.Helpers;
using CyberGameTime.Bussiness.Helpers.Conectivity;
using CyberGameTime.Bussiness.Hubs;
using CyberGameTime.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.BackGroundTask;

public class ValidationPowerOffBAckGroundTask(IMediator _mediator,IMapper _mapper, IServiceProvider _serviceProvider) : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISendMessageSignalR>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var currentDate = DateTime.Now;
            var minutsToWait= 1 * 30 * 1000;
            await Task.Delay(minutsToWait);

            var _allScreens = await _mediator.Send(new GetScreenListQuery());

            var _scrernsWithoutRent = _allScreens
                .Where(x => x.CurrentRentalScrean is null || 
                            (currentDate < x.CurrentRentalScrean.StartDate  || currentDate > x.CurrentRentalScrean.EndDate))
                .Select(x => _mapper.Map<Screens>(x));

            foreach (var _screen in _scrernsWithoutRent)
            {
                try
                {
                    var conn = ConnectivityConstructor.constructor(_screen);
                    if ((await conn.GetStatus()) == Entities.enums.Status.PowerOn)
                    {
                        await conn.TurnOff();
                        await sender.SendPowerOff(_screen.Name);
                        Console.WriteLine("Turned off");
                    }

                }
                catch (Exception ex)
                {
                    await sender.SendError(_screen.Name);
                }

            }
        }


       

    }
}
