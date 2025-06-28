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

public class GetScreenByIpHandler(IGenericRepository<Screens>_repository) : IRequestHandler<GetScreenByIpQuery, Screens>
{
    public async Task<Screens> Handle(GetScreenByIpQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetAll().FirstOrDefault(X=>X.IpAddres==request.ip);
    }
}
