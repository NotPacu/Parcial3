// Archivo: Controllers/TorneosApiController.cs
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;  // **Importante: System.Web.Http para Web API en MVC**
using Parcial3.Models;
using Parcial3.DTOs;
using Parcial3.Data;
using AutoMapper;
using System.Web.Http.Description; // Para documentar la API

namespace Parcial3.Controllers
{
    public class TorneosApiController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private readonly IMapper _mapper = AutoMapperConfig.Configure().CreateMapper(); // Inicializa AutoMapper

        // GET: api/TorneosApi
        public IQueryable<TorneoReadDto> GetTorneos()
        {
            return _context.Torneos.ToList().Select(Mapper.Map<TorneoReadDto>).AsQueryable();
        }

        // GET: api/TorneosApi/5
        [ResponseType(typeof(TorneoReadDto))]
        public IHttpActionResult GetTorneo(int id)
        {
            Torneo torneo = _context.Torneos.Find(id);
            if (torneo == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<TorneoReadDto>(torneo));
        }

        // GET: api/TorneosApi/Buscar?tipo=Futbol&nombre=Copa
        [HttpGet]
        [Route("api/TorneosApi/Buscar")]
        public IQueryable<TorneoReadDto> BuscarTorneos(string tipo = null, string nombre = null, DateTime? fecha = null)
        {
            IQueryable<Torneo> query = _context.Torneos;

            if (!string.IsNullOrEmpty(tipo))
            {
                query = query.Where(t => t.TipoTorneo.Contains(tipo));
            }

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(t => t.NombreTorneo.Contains(nombre));
            }

            if (fecha.HasValue)
            {
                query = query.Where(t => t.FechaTorneo == fecha.Value);
            }

            return query.ToList().Select(Mapper.Map<TorneoReadDto>).AsQueryable();
        }

        // PUT: api/TorneosApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTorneo(int id, TorneoUpdateDto torneoUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0)
            {
                return BadRequest("El ID del torneo debe ser mayor que cero.");
            }

            var torneo = _context.Torneos.Find(id);
            if (torneo == null)
            {
                return NotFound();
            }

            Mapper.Map(torneoUpdateDto, torneo);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TorneoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TorneosApi
        [ResponseType(typeof(TorneoReadDto))]