using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using TiendaServicios.Api.Librerias.Modelo;

namespace TiendaServicios.Api.Librerias.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Correo { get; set; }
            public string Direccion { get; set; }
            public string Telefono { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(p => p.Nombre).NotEmpty();
                RuleFor(p => p.Correo).NotEmpty();
                RuleFor(p => p.Direccion).NotEmpty();
                RuleFor(p => p.Telefono).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            public Manejador()
            {

            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = "Gvh9e1qdp6zG2yJ9BNo3eTpWD2j6XvlUCnVuIX17",
                    BasePath = "https://tiendaservicios-e08e6-default-rtdb.firebaseio.com/"
                };

                FirebaseClient _client = new FireSharp.FirebaseClient(config); ;
                var libreria = new Libreria
                {
                    Nombre = request.Nombre,
                    Correo = request.Correo,
                    Direccion = request.Direccion,
                    Telefono = request.Telefono
                };

                try
                {
                    var data = libreria;
                    PushResponse response = _client.Push("Librerias/", data);
                    data.Id = response.Result.name;
                    SetResponse setResponse = _client.Set("Librerias/" + data.Id, data);

                    if (setResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return Unit.Value;
                    }
                    else
                    {
                        throw new Exception("No se pudo insertar la libreria");
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("No se pudo insertar la libreria");
                }
            }
        }
    }
}
