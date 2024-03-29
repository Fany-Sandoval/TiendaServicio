﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicio.CarritodeCompra.Modelo;
using TiendaServicio.CarritodeCompra.Persitencia;

namespace TiendaServicio.CarritodeCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public string UserName { get; set; }
            public List<string> ProductoLista { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _context;
            public Manejador(CarritoContexto context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request,
                CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion,
                    UserName = request.UserName,
                };
                _context.CarritoSesiones.Add(carritoSesion);
                var result = await _context.SaveChangesAsync();
                if (result == 0)
                {
                    throw new Exception("No se pudo insertar");
                }
                int id = carritoSesion.CarritoSesionId;
                foreach (var p in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = p
                        
                    };
                    _context.CarritoSesionDetalle.Add(detalleSesion);
                }
                var value = await _context.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el detalle");
            }
        }
    }
}
