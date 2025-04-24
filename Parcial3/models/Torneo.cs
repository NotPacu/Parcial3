// Archivo: Models/Torneo.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcial3.Models  // **IMPORTANTE: Ajusta el namespace**
{
    public class Torneo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idTorneos { get; set; }

        [Required(ErrorMessage = "El ID del administrador es requerido.")]
        public int idAdministradorITM { get; set; }

        [Required(ErrorMessage = "El tipo de torneo es requerido.")]
        [MaxLength(30, ErrorMessage = "El tipo de torneo no puede exceder los 30 caracteres.")]
        public string TipoTorneo { get; set; }

        [Required(ErrorMessage = "El nombre del torneo es requerido.")]
        [MaxLength(30, ErrorMessage = "El nombre del torneo no puede exceder los 30 caracteres.")]
        public string NombreTorneo { get; set; }

        [Required(ErrorMessage = "El nombre del equipo es requerido.")]
        [MaxLength(30, ErrorMessage = "El nombre del equipo no puede exceder los 30 caracteres.")]
        public string NombreEquipo { get; set; }

        [Required(ErrorMessage = "El valor de la inscripción es requerido.")]
        [Range(0, int.MaxValue, ErrorMessage = "El valor de la inscripción debe ser un número positivo.")]
        public int ValorInscripcion { get; set; }

        [Required(ErrorMessage = "La fecha del torneo es requerida.")]
        public DateTime FechaTorneo { get; set; }

        [Required(ErrorMessage = "Los integrantes del equipo son requeridos.")]
        [MaxLength(500, ErrorMessage = "La lista de integrantes no puede exceder los 500 caracteres.")]
        public string Integrantes { get; set; }
    }
}