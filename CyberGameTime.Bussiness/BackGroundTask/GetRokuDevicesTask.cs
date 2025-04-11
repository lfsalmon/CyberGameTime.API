using CyberGameTime.Bussiness.Commands.Screen;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CyberGameTime.Bussiness.Commands.Roku;
using CyberGameTime.Bussiness.Helpers.Conectivity.Roku;

namespace CyberGameTime.Bussiness.BackGroundTask;

public class GetRokuDevicesTask : BackgroundService
{

    private RokuScanner _findRokuService;
    private readonly IMediator _mediator;

    public GetRokuDevicesTask(RokuScanner findRokuService, IMediator mediator)
    {
        _findRokuService = findRokuService ?? throw new ArgumentNullException(nameof(findRokuService));
        _mediator = mediator?? throw new ArgumentNullException(nameof(mediator));
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _mediator.Send(new FindDivicesInRedQuery());
        Console.WriteLine("Find All devices");

        
    }

}
