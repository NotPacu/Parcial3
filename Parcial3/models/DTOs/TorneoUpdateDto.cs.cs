using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Parcial3.DTOs  // **IMPORTANTE: Ajusta el namespace**
{
    public class TorneoUpdateDto
    {
        [MaxLength(30, ErrorMessage = "El tipo de torneo no puede exceder los 30 caracteres.")]
        public string? TipoTorneo { get; set; }

        [MaxLength(30, ErrorMessage = "El nombre del torneo no puede exceder los 30 caracteres.")]
        public string? NombreTorneo { get; set; }

        [MaxLength(30, ErrorMessage = "El nombre del equipo no puede exceder los 30 caracteres.")]
        public string? NombreEquipo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El valor de la inscripción debe ser un número positivo.")]
        public int? ValorInscripcion { get; set; }

        public DateTime? FechaTorneo { get; set; }

        [MaxLength(500, ErrorMessage = "La lista de integrantes no puede exceder los 500 caracteres.")]
        public string? Integrantes { get; set; }
    }
}