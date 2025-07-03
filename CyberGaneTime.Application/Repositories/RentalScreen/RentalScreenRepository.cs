using CyberGameTime.Application.Repositories.Common;
using CyberGameTime.Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberGameTime.Application.Repositories.RentalScreen;

public class RentalScreenRepository : GenericRepository<RentalScreens>, IRentalScreenRepository
{
    readonly ILogger<RentalScreenRepository> _logger;
    public RentalScreenRepository(ILogger<GenericRepository<RentalScreens>> loggerBase,
                                    ILogger<RentalScreenRepository> logger,
                                    IConfiguration configuration) : base(loggerBase, configuration)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<RentalScreens>> GetCurrentRentalScreens()
    {
        var _dateTime=DateTime.UtcNow;
        return _context.RentalScreens.Where(x => _dateTime >= x.StartDate &&  _dateTime<= x.EndDate).ToList();
    }
}
