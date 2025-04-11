using AutoMapper;
using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Handler.Screen
{
    public class CreateScreenHandler(IGenericRepository<Screens> _repositoty, IMediator _mediator, IMapper _mapper) : IRequestHandler<AddScreanQuery, ScreenDto>
    {
        public async Task<ScreenDto> Handle(AddScreanQuery _request, CancellationToken cancellationToken)
        {
            //validate
            var _entity = _mapper.Map<Screens>(_request);
            var _entity_save = await _repositoty.Add(_entity);
            return _mapper.Map<ScreenDto>(_entity_save);
        }
    }
}
