﻿using System;
using System.Collections.Generic;

namespace TiendaServicio.CarritodeCompra.Modelo
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public string UserName { get; set; }
        public ICollection<CarritoSesionDetalle> ListaDetalle { get; set; }
       
    }
}
