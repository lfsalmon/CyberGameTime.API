using AutoMapper;
using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Application.Repositories.RentalScreen;
using CyberGameTime.Bussiness.Commands.RentalScreen;
using CyberGameTime.Bussiness.Dtos.RentalScreen;
using CyberGameTime.Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Handler.RentalScreen;

public class GetRentalScreenByDateHandler(IRentalScreenRepository _reposiory, IMapper _mapper) : IRequestHandler<GetRentalScreenQueryByDate, IEnumerable<RentalScreanDto>>
{
    public async Task<IEnumerable<RentalScreanDto>> Handle(GetRentalScreenQueryByDate request, CancellationToken cancellationToken)
    {
        return (await _reposiory.GetCurrentRentalScreens()).Select(x=>_mapper.Map<RentalScreanDto>(x));
    }
}
