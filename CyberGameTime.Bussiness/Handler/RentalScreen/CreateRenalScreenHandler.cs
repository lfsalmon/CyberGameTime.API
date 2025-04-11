using AutoMapper;
using CyberGameTime.Application.Repositories.Common;
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

public class CreateRenalScreenHandler(IGenericRepository<RentalScreens> _reqpository, IMapper _mapper) : IRequestHandler<AddRentalScreenRequest, RentalScreanDto>
{
    public async Task<RentalScreanDto> Handle(AddRentalScreenRequest request, CancellationToken cancellationToken)
    {
        try 
        {
            var _entity = _mapper.Map<RentalScreens>(request);
            var _entitydata=await _reqpository.Add(_entity);
            var _dto=_mapper.Map<RentalScreanDto>(_entitydata);
            return _dto;
        }
        catch (Exception e) 
        {
            // need to add logs data 
            return null;
        }
        
    }
}
