using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Parcial3.Models;
using Parcial3.Services;
using System.Security.Claims; // Para obtener el idAdministrador del token
using Parcial3.DTOs; // Usar los DTOs

namespace Parcial3.Controllers
{
    [RoutePrefix("api/torneos")]
    public class TorneoController : ApiController
    {
        private readonly TorneoService torneoService;
        private readonly AuthService authService;

        public TorneoController()
        {
            this.torneoService = new TorneoService();
            this.authService = new AuthService();
        }

        [Route("")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult ConsultarTorneos()
        {
            try
            {
                List<Torneos> torneos = torneoService.ConsultarTorneos();
                if (torneos == null)
                {
                    return NotFound(); // 404 si no hay torneos
                }
                return Ok(torneos); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in ConsultarTorneos: {ex.Message}");
                return InternalServerError(); // 500 Internal Server Error
            }
        }

        [Route("{id}")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult ConsultarTorneo(int id)
        {
            try
            {
                var torneo = torneoService.ConsultarTorneo(id);
                if (torneo == null)
                {
                    return NotFound(); // 404 si no existe
                }
                return Ok(torneo); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in ConsultarTorneo: {ex.Message}");
                return InternalServerError(); // 500 Internal Server Error
            }
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult CrearTorneo([FromBody] TorneoCreateDto torneoDto) // Usar TorneoCreateDto
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request si la validación falla
            }

            try
            {
                // Obtener el idAdministradorITM del token
                var identity = (ClaimsIdentity)User.Identity;
                var idAdministradorClaim = identity.FindFirst("idAdministradorITM"); // El claim debe llamarse así
                if (idAdministradorClaim == null || !int.TryParse(idAdministradorClaim.Value, out int idAdministradorITM))
                {
                    return BadRequest("ID de administrador no válido en el token."); // 400
                }

                // Mapear el DTO a la entidad Torneos
                Torneos torneo = new Torneos
                {
                    idAdministradorITM = idAdministradorITM,
                    TipoTorneo = torneoDto.TipoTorneo,
                    NombreTorneo = torneoDto.NombreTorneo,
                    NombreEquipo = torneoDto.NombreEquipo,
                    ValorInscripcion = torneoDto.ValorInscripcion,
                    FechaTorneo = torneoDto.FechaTorneo,
                    Integrantes = torneoDto.Integrantes
                };


                torneoService.CrearTorneo(torneo);
                return CreatedAtRoute("DefaultApi", new { id = torneo.idTorneos }, torneo); // 201 Created
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in CrearTorneo: {ex.Message}");
                return InternalServerError(); // 500
            }
        }

        [Route("")]
        [HttpPut]
        [Authorize]
        public IHttpActionResult ActualizarTorneo([FromBody] TorneoUpdateDto torneoDto) // Usar TorneoUpdateDto
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400
            }

            try
            {
                var torneoExistente = torneoService.ConsultarTorneo(torneoDto.idTorneos); // Buscar por DTO
                if (torneoExistente == null)
                {
                    return NotFound(); // 404
                }

                // Mapear los datos del DTO a la entidad existente
                torneoExistente.TipoTorneo = torneoDto.TipoTorneo ?? torneoExistente.TipoTorneo;
                torneoExistente.NombreTorneo = torneoDto.NombreTorneo ?? torneoExistente.NombreTorneo;
                torneoExistente.NombreEquipo = torneoDto.NombreEquipo ?? torneoExistente.NombreEquipo;
                torneoExistente.ValorInscripcion = torneoDto.ValorInscripcion ?? torneoExistente.ValorInscripcion;
                torneoExistente.FechaTorneo = torneoDto.FechaTorneo ?? torneoExistente.FechaTorneo;
                torneoExistente.Integrantes = torneoDto.Integrantes ?? torneoExistente.Integrantes;


                torneoService.ActualizarTorneo(torneoExistente);
                return Ok(torneoExistente); // 200 OK, devolver el objeto actualizado
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in ActualizarTorneo: {ex.Message}");
                return InternalServerError(); // 500
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult EliminarTorneo(int id)
        {
            try
            {
                var torneo = torneoService.ConsultarTorneo(id);
                if (torneo == null)
                {
                    return NotFound(); // 404
                }
                torneoService.EliminarTorneo(id);
                return Ok(torneo); // 200 OK, devolver el objeto eliminado
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in EliminarTorneo: {ex.Message}");
                return InternalServerError(); // 500
            }
        }

        [Route("buscar")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult BuscarTorneos(string tipo = null, string nombre = null, DateTime? fecha = null)
        {
            try
            {
                List<Torneos> torneos = torneoService.BuscarTorneos(tipo, nombre, fecha);
                if (torneos == null || torneos.Count == 0)
                {
                    return NotFound(); // 404 si no se encuentran torneos
                }
                return Ok(torneos); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in BuscarTorneos: {ex.Message}");
                return InternalServerError(); // 500
            }
        }


        [Route("crearUsuarioDePrueba")]
        [HttpPost]
        public IHttpActionResult CrearUsuarioDePrueba(AdministradorITM admin)
        {
            try
            {
                admin.idAministradorITM = int.Parse(admin.Documento);
                admin.Clave = AuthService.GenerarHash(admin.Clave);

                torneoService.CrearUsuarioDePrueba(admin);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CrearUsuarioDePrueba: {ex.Message}");
                return InternalServerError();
            }
        }
    }
}