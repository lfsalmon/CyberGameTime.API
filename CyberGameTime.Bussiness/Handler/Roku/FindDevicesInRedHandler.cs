using CyberGameTime.Bussiness.Commands.Roku;
using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Bussiness.Helpers.Conectivity.Roku;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Handler.Roku
{
    public class FindDevicesInRedHandler(RokuScanner _findRokuService, IMediator _mediator) : IRequestHandler<FindDivicesInRedQuery, IEnumerable<ScreenDto>>
    {
        public async Task<IEnumerable<ScreenDto>> Handle(FindDivicesInRedQuery request, CancellationToken cancellationToken)
        {
            var rokuDevices = await _findRokuService.FindRokuDevices();

            var _listOfDevices = new List<ScreenDto>();

            foreach (var device in rokuDevices)
            {
            //    var _newScreen = RokuRequest.getInfo(device).Result.CreateRequest(device);
                

            //    var _screen = await _mediator.Send(_newScreen);

            //    if (_screen != null) 
            //        _listOfDevices.Add(_screen);
            //    else
            //        Console.WriteLine($"Some error when try to add Device with IP {device} ");
            //    Console.WriteLine($"Device with IP {_screen.IpAddres} and name {_screen.Name} was created correctly");
            }

            return _listOfDevices;
        }
    }
}
