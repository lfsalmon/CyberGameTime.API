using CyberGameTime.Entities.Models;
using CyberGameTime.Entities.Models.historics;
using CyberGameTime.Models;
using Microsoft.EntityFrameworkCore;


namespace CyberGameTime.Application; 

public class CyberGameContext : DbContext
{

    // Migrations: dotnet ef migrations add AddConsoleType --project ./CyberGaneTime.Application --startup-project .\CyberGameTime.API
    public DbSet<Screens> ScreenDevices { get; set; }
    public DbSet<RentalScreens> RentalScreens { get; set; }
    public DbSet<ScreenHistorics> ScreenHistorics { get; set; }


    public CyberGameContext(DbContextOptions<CyberGameContext> options)
    : base(options)
    {
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RentalScreens>(entity =>
        {
            entity.HasOne(a => a.Screens)
                  .WithMany(a => a.RentalScreens)
                  .HasForeignKey(a => a.ScreenId);
        });
    }
}
