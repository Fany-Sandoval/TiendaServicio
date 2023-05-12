using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using TiendaServicio.CarritodeCompra.Persitencia;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TiendaServicio.CarritodeCompra.RemoteInterface;

namespace TiendaServicio.CarritodeCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContexto carritoContexto;
            private readonly ILibroService libroService;
            public Manejador(CarritoContexto _carritoContexto, ILibroService _libroService)
            {
                carritoContexto = _carritoContexto;
                libroService = _libroService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await carritoContexto.CarritoSesiones.
                FirstOrDefaultAsync(x => x.CarritoSesionId ==
                request.CarritoSesionId);

                var carritoSessionDetalle = await carritoContexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId ==
                request.CarritoSesionId).ToListAsync();
                var listaCarritoDto = new List<CarritoDetalleDdto>();

                foreach (var libro in carritoSessionDetalle)
                {
                    var response = await libroService.GetLibro(new System.Guid(libro.ProductoSeleccionado));
                    if (response.resultado)
                    {
                        var objetoLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDdto
                        {
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = objetoLibro.LibreriaMateriaId,
                            Precio = objetoLibro.Precio,

                        };
                        listaCarritoDto.Add(carritoDetalle);
                    }

                }
                    var carritoSessionDto = new CarritoDto
                    {
                        CarritoId = carritoSesion.CarritoSesionId,
                        FechaCreacionSesion = carritoSesion.FechaCreacion,
                        ListaDeProductos = listaCarritoDto,
                        UserName = carritoSesion.UserName
                        

                    };

                   

                
                return carritoSessionDto;
            }
        }

    }
}
