using AutoMapper;
using CyberGameTime.Bussiness.Commands.Conectivity;
using CyberGameTime.Bussiness.Commands.Roku;
using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Bussiness.Helpers.Conectivity;
using CyberGameTime.Bussiness.Helpers.Conectivity.Roku;
using CyberGameTime.Bussiness.Responses;
using CyberGameTime.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Handler.Conectivity
{
    public class ConectivityHandler(IMapper _mapper) : IRequestHandler<ConectivityScreenQuery, DeviceResponseData>
    {
        public async Task<DeviceResponseData?> Handle(ConectivityScreenQuery request, CancellationToken cancellationToken)
        {
            var _screen=_mapper.Map<Screens>(request);
            var _connect = ConnectivityConstructor.constructor(_screen);
            return await _connect.getInfo();
        }
    }
}
