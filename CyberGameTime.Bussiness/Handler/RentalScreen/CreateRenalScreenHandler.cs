using AutoMapper;
using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Bussiness.Commands.RentalScreen;
using CyberGameTime.Bussiness.Dtos.RentalScreen;
using CyberGameTime.Bussiness.Helpers.Conectivity;
using CyberGameTime.Entities.Models;
using CyberGameTime.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Bussiness.Handler.RentalScreen;

public class CreateRenalScreenHandler(IGenericRepository<RentalScreens> _reqpository, IGenericRepository<Screens> _reqpositoryScreens, IMapper _mapper) : IRequestHandler<AddRentalScreenRequest, RentalScreanDto>
{
    public async Task<RentalScreanDto> Handle(AddRentalScreenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var _entity = _mapper.Map<RentalScreens>(request);
            var _entitydata = await _reqpository.Add(_entity);
            var _dto = _mapper.Map<RentalScreanDto>(_entitydata);

            
            try
            {
                var _screen = _reqpositoryScreens.GetByPredicate(x => x.Id == request.ScreenId).FirstOrDefault();
                var _connect = ConnectivityConstructor.constructor(_screen);
                await _connect.TurnOn();

            }
            catch
            {
                // nose predio 

            }
            return _dto;
        }
        catch (Exception e)
        {
            // need to add logs data 
            return null;
        }
        
    }
}
