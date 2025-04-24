using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Parcial3.Models;

namespace Parcial3.Services
{
    public class TorneoService
    {

        private readonly DBExamenEntities _context;

        public TorneoService()
        {
            _context = new DBExamenEntities();
        }

        public List<Torneos> ConsultarTorneos()
        {
            return _context.Torneos.ToList();
        }

        public Torneos ConsultarTorneo(int id)
        {
            return _context.Torneos.FirstOrDefault(t => t.idTorneos == id);
        }

        public void CrearTorneo(Torneos torneo)
        {
            _context.Torneos.Add(torneo);
            _context.SaveChanges();
        }

        public void ActualizarTorneo(Torneos torneo)
        {
            var torneoExistente = _context.Torneos.FirstOrDefault(t => t.idTorneos == torneo.idTorneos);
            if (torneoExistente != null)
            {
                _context.Torneos.AddOrUpdate(torneo);
                _context.SaveChanges();
            }
        }

        public void EliminarTorneo(int id)
        {
            var torneo = _context.Torneos.FirstOrDefault(t => t.idTorneos == id);
            if (torneo != null)
            {
                _context.Torneos.Remove(torneo);
                _context.SaveChanges();
            }
        }
    }
}