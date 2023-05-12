using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using TiendaServicio.CarritodeCompra.Persitencia;
using TiendaServicio.CarritodeCompra.RemoteInterface;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.CarritodeCompra.RemoteServices;

namespace TiendaServicio.CarritodeCompra.Aplicacion
{
    public class ConsultaClase
    {
        public class ListaCarrito : IRequest<List<CarritoDto>>
        {
        }

        public class Manejador : IRequestHandler<ListaCarrito, List<CarritoDto>>
        {
            private readonly CarritoContexto carritoContexto;
            private readonly ILibroService librosService;

            public Manejador(CarritoContexto _carritoContexto, ILibroService _librosService)
            {
                carritoContexto = _carritoContexto;
                librosService = _librosService;
            }

            public async Task<List<CarritoDto>> Handle(ListaCarrito request, CancellationToken cancellationToken)
            {
                var carritosSesion = await carritoContexto.CarritoSesiones.ToListAsync();




                var listaCarritosDto = new List<CarritoDto>();

                foreach (var carritoSesion in carritosSesion)
                {
                    //Devuelve la Lista de Productos Detalle Solo para Conocer cada Detalle
                    var carritoSesionDetalle = await carritoContexto.CarritoSesionDetalle
                        .Where(x => x.CarritoSesionId == carritoSesion.CarritoSesionId).ToListAsync();

                    var listaCarritoDetalleDdto = new List<CarritoDetalleDdto>();

                    foreach (var libro in carritoSesionDetalle)
                    {
                        //Invocamos al Microservicio Externo
                        var response = await librosService.GetLibro(new System.Guid(libro.ProductoSeleccionado));

                        if (response.resultado)
                        {
                            //Se Accede si se Encuentra Algo en la Base de Datos
                            var objetoLibro = response.Libro; //Retorno un libroRemote
                            var carritoDetalle = new CarritoDetalleDdto
                            {
                                TituloLibro = objetoLibro.Titulo,
                                FechaPublicacion = objetoLibro.FechaPublicacion,
                                Precio = objetoLibro.Precio,
                                LibroId = objetoLibro.LibreriaMateriaId
                            };

                            listaCarritoDetalleDdto.Add(carritoDetalle);
                        }
                    }

                    //Llenamos el Objeto que Realmente es Necesario Retomar
                    var carritoDto = new CarritoDto
                    {
                        CarritoId = carritoSesion.CarritoSesionId,
                        FechaCreacionSesion = carritoSesion.FechaCreacion,
                        ListaDeProductos = listaCarritoDetalleDdto,
                        UserName = carritoSesion.UserName

                    };

                    listaCarritosDto.Add(carritoDto);
                }

                return listaCarritosDto;
            }
        }
    }
}
