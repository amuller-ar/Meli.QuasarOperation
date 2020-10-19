using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuasarOperation.Domain.Contracts.Model
{
    public class TransmissionContract
    {
        /// <summary>
        /// Nombre del satelite
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Distancia
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Mensaje recibido
        /// </summary>
        [Required]
        public string[] Message { get; set; }
    }
}
