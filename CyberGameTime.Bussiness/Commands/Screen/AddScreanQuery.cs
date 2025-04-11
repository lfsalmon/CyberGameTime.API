using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Entities.enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Commands.Screen
{
    public class AddScreanQuery : IRequest<ScreenDto>
    {
        public string Name { get; set; }
        public string IpAddres { get; set; }
        public string Roku_DeviceId { get; set; }
        public string Roku_SerialNumber { get; set; }
        public string Roku_udn { get; set; }
        public ConsoleType ConsoleType { get; set; }
        public ConnectionType ConnectionType { get; set; }
    }
}
