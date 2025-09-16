using CyberGameTime.Application;
using CyberGameTime.Entities.enums;
using CyberGameTime.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace CyberGameTime.Seeding;

/// <summary>
/// Seed del DIA DE AYER tomando como referencia la hora local de Ciudad de México.
/// - Cálculo en hora local MX: rango [ayer 00:00 MX, hoy 00:00 MX)
/// - Al GUARDAR, todas las fechas se convierten a UTC.
/// - Para cada pantalla crea primera renta a las 12:00 (hora MX) con duración aleatoria y rentas adicionales sin traslapes.
/// - Solo guarda logs PowerOff cada 20 minutos cuando NO hay una renta activa en ese instante (en hora MX), guardando en UTC.
/// - Idempotente: elimina registros que se traslapen con el rango (comparando en UTC).
/// </summary>
public static class DataSeeder
{
    private static TimeZoneInfo GetMexicoTimeZone()
    {
        // Preferir Windows ID para Ciudad de México; si falla, usar IANA en Linux; fallback a Central Standard Time
        string[] ids =
        {
            "Central Standard Time (Mexico)", // Windows - Ciudad de México
            "America/Mexico_City",           // IANA - Linux/macOS
            "Central Standard Time"           // Fallback (EE.UU. Central)
        };

        foreach (var id in ids)
        {
            try { return TimeZoneInfo.FindSystemTimeZoneById(id); }
            catch { /* try next */ }
        }

        return TimeZoneInfo.Local; // último recurso
    }

    private static DateTime MexicoToUtc(DateTime localMexico, TimeZoneInfo mxTz)
    {
        // Asegurarnos de tratarlo como tiempo "local MX" sin zona (Unspecified)
        if (localMexico.Kind != DateTimeKind.Unspecified)
            localMexico = DateTime.SpecifyKind(localMexico, DateTimeKind.Unspecified);
        return TimeZoneInfo.ConvertTimeToUtc(localMexico, mxTz);
    }

    public static async Task SeedLastDayAsync(CyberGameContext context, CancellationToken ct = default)
    {
        var mxTz = GetMexicoTimeZone();

        // "Ahora" en hora de Ciudad de México (sin zona adjunta)
        var mexicoNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, mxTz);

        // Día de ayer en hora MX
        var todayStartLocal = mexicoNow.Date;          // hoy 00:00 MX
        var dayStartLocal = todayStartLocal.AddDays(-1); // ayer 00:00 MX
        var dayEndLocal = todayStartLocal;               // límite exclusivo (MX)

        // Convertir rango a UTC para operaciones en la BD (los datos se guardan en UTC)
        var dayStartUtc = MexicoToUtc(dayStartLocal, mxTz);
        var dayEndUtc = MexicoToUtc(dayEndLocal, mxTz);

        // Pantallas
        var screens = await context.ScreenDevices.AsNoTracking().ToListAsync(ct);
        if (screens.Count == 0) return;

        // Eliminar rentas que se traslapen con el rango (comparación en UTC)
        var rentalsToDelete = await context.RentalScreens
            .Where(r => r.StartDate < dayEndUtc && r.EndDate > dayStartUtc) // overlap
            .ToListAsync(ct);
        if (rentalsToDelete.Count > 0)
            context.RentalScreens.RemoveRange(rentalsToDelete);

        // Eliminar logs en el rango (comparación en UTC)
        var logsToDelete = await context.DeviceStatusLog
            .Where(l => (l.CreateAt >= dayStartUtc && l.CreateAt < dayEndUtc) ||
                        (l.UpdateAt != null && l.UpdateAt >= dayStartUtc && l.UpdateAt < dayEndUtc))
            .ToListAsync(ct);
        if (logsToDelete.Count > 0)
            context.DeviceStatusLog.RemoveRange(logsToDelete);

        await context.SaveChangesAsync(ct);

        var rand = new Random();

        // Generar rentas por pantalla (en hora local MX)
        var rentalsByScreen = new Dictionary<long, List<(DateTime Start, DateTime End)>>();
        foreach (var screen in screens)
        {
            var list = new List<(DateTime Start, DateTime End)>();
            var firstStartLocal = dayStartLocal.AddHours(12); // 12:00 ayer (MX)
            if (firstStartLocal < dayEndLocal) // cabe en el día
            {
                var firstDuration = rand.Next(40, 91); // 40-90 min
                var firstEndLocal = firstStartLocal.AddMinutes(firstDuration);
                if (firstEndLocal > dayEndLocal) firstEndLocal = dayEndLocal;
                if (firstEndLocal > firstStartLocal)
                {
                    list.Add((firstStartLocal, firstEndLocal));

                    var lastEndLocal = firstEndLocal;
                    while (true)
                    {
                        // Gap 2-5 horas
                        var gapMinutes = rand.Next(120, 301);
                        var nextStartLocal = lastEndLocal.AddMinutes(gapMinutes);
                        if (nextStartLocal >= dayEndLocal) break;
                        var duration = rand.Next(40, 91);
                        var nextEndLocal = nextStartLocal.AddMinutes(duration);
                        if (nextEndLocal > dayEndLocal) nextEndLocal = dayEndLocal;
                        if (nextEndLocal <= nextStartLocal) break;
                        list.Add((nextStartLocal, nextEndLocal));
                        lastEndLocal = nextEndLocal;
                    }
                }
            }

            rentalsByScreen[screen.Id] = list;

            if (list.Count > 0)
            {
                // Guardar en UTC
                var entities = list.Select(p => new RentalScreens
                {
                    ScreenId = screen.Id,
                    StartDate = MexicoToUtc(p.Start, mxTz),
                    EndDate = MexicoToUtc(p.End, mxTz),
                    CreateAt = MexicoToUtc(p.Start, mxTz),
                    UpdateAt = MexicoToUtc(p.Start, mxTz)
                });
                await context.RentalScreens.AddRangeAsync(entities, ct);
            }
        }

        // Timeline cada 20 minutos (inclusive inicio, exclusivo fin) en hora MX
        for (var tLocal = dayStartLocal; tLocal < dayEndLocal; tLocal = tLocal.AddMinutes(20))
        {
            foreach (var screen in screens)
            {
                var active = rentalsByScreen[screen.Id].Any(r => r.Start <= tLocal && r.End > tLocal);
                if (active) continue; // No log durante renta

                await context.DeviceStatusLog.AddAsync(new DeviceStatusLog
                {
                    ScreenId = screen.Id,
                    ScreenName = screen.Name,
                    IpAddress = screen.IpAddres,
                    Status = Status.PowerOff.ToString(),
                    Message = "Dispositivo inactivo (seed)",
                    ConnectionType = screen.ConnectionType.ToString(),
                    CreateAt = MexicoToUtc(tLocal, mxTz),
                    UpdateAt = MexicoToUtc(tLocal.AddMinutes(20), mxTz)
                }, ct);
            }
        }

        await context.SaveChangesAsync(ct);
    }
}
