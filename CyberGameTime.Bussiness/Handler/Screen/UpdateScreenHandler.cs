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

namespace CyberGameTime.Bussiness.Handler.Screen;

public class UpdateScreenHandler(IGenericRepository<Screens> _repositoty, IMapper _mapper) : IRequestHandler<UpdateScreenRequest, ScreenDto>
{
    public async Task<ScreenDto> Handle(UpdateScreenRequest _request, CancellationToken cancellationToken)
    {
        var _entity = _repositoty.FindById(_request.Id);
        if (_entity is null) return null;

        _entity.Name= _request.Name;
        _entity.ConsoleType= _request.ConsoleType;

        var _entity_save = await _repositoty.Update(_entity);
        return _mapper.Map<ScreenDto>(_entity_save);
    }
}
