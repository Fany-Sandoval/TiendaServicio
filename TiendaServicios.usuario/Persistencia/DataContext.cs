using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TiendaServicios.usuario.Modelo;

namespace TiendaServicios.usuario.Persistencia
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {



        }
        //declaracion de la tabla
        public DbSet<Users>? Users { get; set; }


    }
}
