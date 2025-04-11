using AutoMapper;
using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Bussiness.Commands.Screen;
using CyberGameTime.Bussiness.Dtos.Screen;
using CyberGameTime.Models;
using MediatR;
using Microsoft.Graph.Security.Alerts_v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Handler.Screen;

public class GetScreenByIdHandler(IGenericRepository<Screens>_repository, IMapper _mapper) : IRequestHandler<GetScreenByIdQuery, ScreenDto>
{
    public async Task<ScreenDto> Handle(GetScreenByIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<ScreenDto>(_repository.FindById(request.id));
    }
}
