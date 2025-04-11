using CyberGameTime.Bussiness.Dtos.RentalScreen;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Commands.RentalScreen;

public class GetRentalScreenQueryByDate:IRequest<IEnumerable<RentalScreanDto>>;

