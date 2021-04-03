
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// importamos esta dependencia creamos primero la carpeta models y el modelo y de hay la base de datos
using Microsoft.EntityFrameworkCore;
using TarjetasBacked.Models;
namespace TarjetasBacked
{
    //tenemos que heredar de DbContext
    public class AplicationDBContext:DbContext
    {
        public DbSet<TarjetaCredito> TarjetaCredito { get; set; }
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options) {
        }
    }
}
