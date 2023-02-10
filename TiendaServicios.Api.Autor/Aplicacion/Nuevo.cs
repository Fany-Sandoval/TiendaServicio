using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        //realiza insersiones
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }
        //valida la clase ejecuta
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion() { 
                RuleFor(p=> p.Nombre).NotEmpty();
                RuleFor(p=> p.Apellido).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _context;
            public Manejador(ContextoAutor context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //se crea la instancia
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                };
                //agregamos el objeto del tipo 
                _context.AutorLibros.Add(autorLibro);
                //insercciones
                var respuesta = await _context.SaveChangesAsync();
                if(respuesta >0)
                {
                    return Unit.Value;
                }
                throw new Exception("Valio No se Inserto");
            }
        }
    }
}
