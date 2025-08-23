using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Commands.Screen;

public record GetScreenByIpQuery(string ip):IRequest<Screens>;
