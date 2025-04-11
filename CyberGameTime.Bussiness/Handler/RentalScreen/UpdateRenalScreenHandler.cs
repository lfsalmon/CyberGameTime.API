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

public class UpdateRenalScreenHandler(IGenericRepository<RentalScreens> _repositoty, IMapper _mapper) : IRequestHandler<UpdateRentalScreenCommand, RentalScreanDto>
{
    public async Task<RentalScreanDto> Handle(UpdateRentalScreenCommand _request, CancellationToken cancellationToken)
    {
        try 
        {
            var _entity = _repositoty.FindById(_request.Id);
            if (_entity is null) return null;

            _entity.UpdateAt=DateTime.UtcNow;
            _entity.EndDate= DateTime.UtcNow;

            var _entitydata=await _repositoty.Update(_entity);
            return _mapper.Map<RentalScreanDto>(_entitydata);
            
        }
        catch (Exception e) 
        {
            // need to add logs data 
            return null;
        }
        
    }
}
