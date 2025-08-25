using CyberGameTime.Bussiness.Dtos.Screen;
using MediatR;
using System.Collections.Generic;

namespace CyberGameTime.Bussiness.Commands.Screen
{
    // Query to scan network and return (and possibly create) screens
    public record ScanDevicesQuery : IRequest<uint>;
}
