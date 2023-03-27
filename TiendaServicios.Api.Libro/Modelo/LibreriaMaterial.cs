using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaServicios.Api.Libro.Modelo
{
    public class LibreriaMaterial
    {
        [Key]
        public Guid? LibreriaMateriaId { get; set; } 
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public double Precio { get; set; }
        public Guid? AutorLibro { get; set; }   
    }
}
