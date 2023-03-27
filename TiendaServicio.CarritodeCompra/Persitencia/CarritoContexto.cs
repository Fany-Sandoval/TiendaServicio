using Microsoft.EntityFrameworkCore;
using TiendaServicio.CarritodeCompra.Modelo;

namespace TiendaServicio.CarritodeCompra.Persitencia
{
    public class CarritoContexto : DbContext
    {
        public CarritoContexto(DbContextOptions<CarritoContexto> options) : base(options)
        {

        }
        public DbSet<CarritoSesion> CarritoSesiones { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    


    }
}
