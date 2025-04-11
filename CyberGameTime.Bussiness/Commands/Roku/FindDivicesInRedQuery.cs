using CyberGameTime.Bussiness.Dtos.Screen;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Commands.Roku
{
    public record FindDivicesInRedQuery:IRequest<IEnumerable<ScreenDto>>;
}
