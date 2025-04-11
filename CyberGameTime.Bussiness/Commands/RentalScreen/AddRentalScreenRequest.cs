using CyberGameTime.Bussiness.Dtos.RentalScreen;
using MediatR;


namespace CyberGameTime.Bussiness.Commands.RentalScreen;

public record AddRentalScreenRequest: IRequest<RentalScreanDto>
{
    public  required long ScreenId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
}

