using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Application.Repositories.RentalScreen;

public interface IRentalScreenRepository: IGenericRepository<RentalScreens>
{
    Task<IEnumerable<RentalScreens>> GetCurrentRentalScreens();
}
