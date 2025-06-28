using AutoMapper;
using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Bussiness.Commands.RentalScreen;
using CyberGameTime.Bussiness.Dtos.RentalScreen;
using CyberGameTime.Entities.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Handler.RentalScreen;

public class UpdateRenalScreenHandler(IGenericRepository<RentalScreens> _repositoty, IMapper _mapper,ILogger<UpdateRenalScreenHandler> _logger) : IRequestHandler<UpdateRentalScreenCommand, RentalScreanDto>
{
    public async Task<RentalScreanDto> Handle(UpdateRentalScreenCommand _request, CancellationToken cancellationToken)
    {
        try 
        {
            var _entity = _repositoty.FindById(_request.Id);
            if (_entity is null) 
            {
                _logger.LogError($"ther is not REntalScreen form the id {_request.Id} ");
                return null;
            }
            
            _entity.UpdateAt=DateTime.Now;
            _entity.EndDate= DateTime.Now;

            var _entitydata=await _repositoty.Update(_entity);
            return _mapper.Map<RentalScreanDto>(_entitydata);
            
        }
        catch (Exception e) 
        {
            _logger.LogError($"some kind of error in this point {e.Message}");
            return null;
        }
        
    }
}
