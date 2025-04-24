using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// Archivo: Data/ApplicationDbContext.cs
using System.Data.Entity;  // **Importante: System.Data.Entity para MVC**
using Parcial3.Models;

namespace Parcial3.Data   
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") // Nombre de la cadena de conexión
        {
        }

        public DbSet<Torneo> Torneos { get; set; }
    }
}