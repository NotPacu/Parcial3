using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Parcial3.Models;
using Parcial3.Services;

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
        public List<Torneos> ConsultarTorneos()
        {
            return torneoService.ConsultarTorneos();
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult ConsultarTorneo(int id)
        {
            var torneo = torneoService.ConsultarTorneo(id);
            if (torneo == null)
            {
                return BadRequest("No existe torneo con ese id");
            }
            return Ok(torneo);
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult CrearTorneo([FromBody] Torneos torneo)
        {
            string cedula = User.Identity.Name;

            AdministradorITM user = authService.BuscarPorCedula(cedula);

            if (user == null)
                return BadRequest("El usuario no existe.");

            if (torneo == null)
                return BadRequest("El torneo no puede ser nulo.");

            torneo.idAdministradorITM = user.idAministradorITM;

            torneoService.CrearTorneo(torneo);
            return Ok(torneo);
        }

        [Route("")]
        [HttpPut]
        [Authorize]
        public IHttpActionResult ActualizarTorneo([FromBody] Torneos torneo)
        {
            if (torneo == null)
                return BadRequest("El torneo no puede ser nulo.");
            var torneoExistente = torneoService.ConsultarTorneo(torneo.idTorneos);
            if (torneoExistente == null)
                return BadRequest("No existe torneo con ese id");
            torneo.idTorneos = torneo.idTorneos;
            torneoService.ActualizarTorneo(torneo);
            return Ok(torneo);
        }

        [Route("{id}")]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult EliminarTorneo(int id)
        {
            var torneo = torneoService.ConsultarTorneo(id);
            if (torneo == null)
                return BadRequest("No existe torneo con ese id");
            torneoService.EliminarTorneo(id);
            return Ok(torneo);
        }


        [Route("crearUsuarioDePrueba")]
        [HttpPost]
        public IHttpActionResult CrearUsuarioDePrueba(AdministradorITM admin)
        {
            DBExamenEntities dBExamenEntities = new DBExamenEntities();
            admin.idAministradorITM = int.Parse(admin.Documento);
            admin.Clave = AuthService.GenerarHash(admin.Clave);

            dBExamenEntities.AdministradorITM.Add(admin);
            dBExamenEntities.SaveChanges();
            return Ok(admin);
        }


    }
}
