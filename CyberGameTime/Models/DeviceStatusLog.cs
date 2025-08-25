using CyberGameTime.Entities.Common;
using CyberGameTime.Entities.enums;
using CyberGameTime.enums;
using System;

namespace CyberGameTime.Entities.Models
{
    /// <summary>
    /// Log de cambios o lecturas de estado de un dispositivo (Screen).
    /// Se registra cada vez que se consulta el estado y el dispositivo está offline, unreachable
    /// u ocurre algún error relevante. También puedes registrar transiciones de estado.
    /// </summary>
    public class DeviceStatusLog : IEntity
    {
        public long Id { get; set; }

        /// <summary>
        /// FK opcional a la pantalla (Screens.Id) si la tienes disponible.
        /// </summary>
        public long? ScreenId { get; set; }

        /// <summary>
        /// Nombre de la pantalla al momento del log (denormalizado para reportes rápidos).
        /// </summary>
        public string? ScreenName { get; set; }

        /// <summary>
        /// Dirección IP usada en la consulta.
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// Estado leído (PowerOn, PowerOff, Undefined, etc.). Puede ser null si no se obtuvo.
        /// </summary>
        public string? Status{ get; set; }

        /// <summary>
        /// Mensjae extra 
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// Tipo de conexión usada (TuyaLan, Roku, etc.).
        /// </summary>
        public string ConnectionType { get; set; }

        /// <summary>
        /// Marca temporal de creación (UTC).
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// Marca temporal de actualización 
        /// </summary>
        public DateTime? UpdateAt { get; set; }
    }
}
