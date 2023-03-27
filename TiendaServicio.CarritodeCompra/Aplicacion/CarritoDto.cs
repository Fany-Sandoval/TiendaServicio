using System;
using System.Collections.Generic;

namespace TiendaServicio.CarritodeCompra.Aplicacion
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }
        public DateTime ? FechaCreacionSesion { get; set; }
        public List<CarritoDetalleDdto> ListaDeProductos { get; set; }
    }
}
