using CyberGameTime.Bussiness.Dtos.RentalScreen;
using CyberGameTime.Bussiness.Responses;
using CyberGameTime.Entities.enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Commands.Conectivity
{
    public class ConectivityScreenQuery : IRequest<DeviceResponseData>
    {
        public string IpAddres { get; set; }
        public ConnectionType ConnectionType { get; set; }
    }
}
