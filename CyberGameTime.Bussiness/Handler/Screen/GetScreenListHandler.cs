using AutoMapper;
using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Bussiness.Commands.RentalScreen;
using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Handler.Screen;

public class GetScreenListHandler(IGenericRepository<Screens> _repositoty, IMapper _mapper,IMediator _mediator) : IRequestHandler<GetScreenListQuery, IEnumerable<ScreenDto>>
{
    public async Task<IEnumerable<ScreenDto>> Handle(GetScreenListQuery request, CancellationToken cancellationToken)
    {
        var _rentalScreens = await _mediator.Send(new GetRentalScreenQueryByDate());

        return _repositoty.GetAll().Select(x => {
            var _screenDto = _mapper.Map<ScreenDto>(x);
            _screenDto.RentalScrean = _rentalScreens.FirstOrDefault(x => x.ScreenId == _screenDto.Id);
            return _screenDto;
        });
    }
}
