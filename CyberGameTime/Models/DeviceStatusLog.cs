using CyberGameTime.Entities.Common;
using CyberGameTime.Entities.enums;
using CyberGameTime.enums;
using System;

namespace CyberGameTime.Entities.Models
{
    /// <summary>
    /// Log de cambios o lecturas de estado de un dispositivo (Screen).
    /// Se registra cada vez que se consulta el estado y el dispositivo est� offline, unreachable
    /// u ocurre alg�n error relevante. Tambi�n puedes registrar transiciones de estado.
    /// </summary>
    public class DeviceStatusLog : IEntity
    {
        public long Id { get; set; }

        /// <summary>
        /// FK opcional a la pantalla (Screens.Id) si la tienes disponible.
        /// </summary>
        public long? ScreenId { get; set; }

        /// <summary>
        /// Nombre de la pantalla al momento del log (denormalizado para reportes r�pidos).
        /// </summary>
        public string? ScreenName { get; set; }

        /// <summary>
        /// Direcci�n IP usada en la consulta.
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// Estado le�do (PowerOn, PowerOff, Undefined, etc.). Puede ser null si no se obtuvo.
        /// </summary>
        public string? Status{ get; set; }

        /// <summary>
        /// Mensjae extra 
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// Tipo de conexi�n usada (TuyaLan, Roku, etc.).
        /// </summary>
        public string ConnectionType { get; set; }

        /// <summary>
        /// Marca temporal de creaci�n (UTC).
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// Marca temporal de actualizaci�n 
        /// </summary>
        public DateTime? UpdateAt { get; set; }
    }
}
