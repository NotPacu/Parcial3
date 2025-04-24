using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Parcial3.DTOs  // **IMPORTANTE: Ajusta el namespace**
{
    public class TorneoReadDto
    {
        public int idTorneos { get; set; }
        public int idAdministradorITM { get; set; }
        public string TipoTorneo { get; set; }
        public string NombreTorneo { get; set; }
        public string NombreEquipo { get; set; }
        public int ValorInscripcion { get; set; }
        public DateTime FechaTorneo { get; set; }
        public string Integrantes { get; set; }
    }
}