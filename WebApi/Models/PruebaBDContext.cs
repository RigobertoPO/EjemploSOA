using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class PruebaBDContext:DbContext
    {
        public DbSet<Empleado> Empleados { get; set; }
        public PruebaBDContext(): base("name=PruebasDbConnection") // Nombre de la cadena de conexión a usar.
         {
            System.Data.Entity.Database.SetInitializer<Models.PruebaBDContext>(null);
        }
    }
}