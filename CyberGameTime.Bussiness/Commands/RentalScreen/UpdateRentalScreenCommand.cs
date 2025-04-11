using CyberGameTime.Bussiness.Dtos.RentalScreen;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Commands.RentalScreen
{
    public class UpdateRentalScreenCommand : IRequest<RentalScreanDto>
    {
        public long Id { get; set; }
    }
}
