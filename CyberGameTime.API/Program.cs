using CyberGameTime.Business;
using CyberGameTime.Application;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Microsoft.EntityFrameworkCore.Infrastructure;
using CyberGameTime.Bussiness.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});
builder.Logging.AddConsole();


string? connectionString = Environment.GetEnvironmentVariable("ConnectionString");
if(string.IsNullOrEmpty(connectionString))
    connectionString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.CyberGameTimelApplication(builder.Configuration);

Console.WriteLine("Cadena de conexi�n usada: " + connectionString);

builder.Services.AddDbContext<CyberGameContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() 
        //.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
              //.AllowCredentials(); 
    });
});


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc; // Siempre UTC con 'Z'
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.CyberGameTimelApplication(builder.Configuration);
builder.Services.CyberGameTimeBussines(builder.Configuration);


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CyberGameContext>();
        var migrator = context.Database.GetService<IMigrator>();

        // Generar y aplicar todas las migraciones en tiempo de ejecuci�n
        await GenerateAndApplyMigrations(context);

        Console.WriteLine("Migraciones aplicadas exitosamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocurri� un error al inicializar la base de datos: {ex.Message}");
    }
}



app.UseRouting();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors("AllowAll");
//app.UseHttpsRedirection();
app.UseWebSockets(); 
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    app.MapHub<MessageHubs>("/messageHub");
});


app.Run();


static async Task GenerateAndApplyMigrations(CyberGameContext context)
{
    var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
    {
        Console.WriteLine("Aplicando migraciones pendientes...");
        await context.Database.MigrateAsync();
        Console.WriteLine("Migraciones aplicadas correctamente.");
    }
    else
    {
        Console.WriteLine("No hay migraciones pendientes.");
    }
}