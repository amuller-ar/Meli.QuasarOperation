using System;

namespace QuasarOperation.Domain
{
    /// <summary>
    /// Satelite Receptor
    /// </summary>
    public class Satellite
    {
        /// <summary>
        /// Nombre del satelite
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Coordenada de ubicación
        /// </summary>
        public Coordinate Location { get; set; }
    }
}
