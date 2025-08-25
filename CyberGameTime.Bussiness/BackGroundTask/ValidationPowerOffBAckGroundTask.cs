using AutoMapper;
using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Handler.Conectivity;
using CyberGameTime.Bussiness.Helpers;
using CyberGameTime.Bussiness.Helpers.Conectivity;
using CyberGameTime.Bussiness.Hubs;
using CyberGameTime.Bussiness.Services;
using CyberGameTime.Entities.enums;
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

public class ValidationPowerOffBAckGroundTask(IMediator _mediator,IMapper _mapper, IServiceProvider _serviceProvider, ILogService logService) : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var sender = scope.ServiceProvider.GetRequiredService<ISendMessageSignalR>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var currentDate = DateTime.UtcNow;
            var minutsToWait= 1 * 30 * 1000;
            await Task.Delay(minutsToWait);
            var _allScreens = await _mediator.Send(new GetScreenListQuery());
            
            var _scrernsWithoutRent = _allScreens
                .Where(x => x.RentalScrean is null || 
                            (currentDate < x.RentalScrean.StartDate  || currentDate > x.RentalScrean.EndDate))
                .Select(x => _mapper.Map<Screens>(x));

            foreach (var _screen in _scrernsWithoutRent)
            {
                var status = Entities.enums.Status.Undefined;
                string Message = string.Empty;
                try
                {
                    var conn = ConnectivityConstructor.constructor(_screen);
                    status = await conn.GetStatus();
                    await logService.AddLog(_screen, status, Message);
                    if ((status) == Entities.enums.Status.PowerOn)
                    {
                        var result =await conn.TurnOff();
                        await sender.SendPowerOff(_screen.Name);
                        await logService.AddLog(_screen, result, Message);
                        Console.WriteLine("Turned off");
                    }

                }
                catch (Exception ex)
                {
                    await sender.SendError(_screen.Name);
                    Message =$"Exception in {nameof(ValidationPowerOffBAckGroundTask)} then the error was {Status.Undefined}" ;
                    await logService.AddLog(_screen, Status.Undefined, Message);
                }
                finally
                {
                    
                }
                
            }
        }


       

    }
}
