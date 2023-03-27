using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading;
using System;
using TiendaServicios.Api.Librerias.Modelo;
using MediatR;
using AutoMapper;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TiendaServicios.Api.Librerias.Aplicacion
{
    public class ConsultarFiltro
    {
        public class LibreriaUnica : IRequest<LibreriaDto>
        {
            public string Id { get; set; }
        }
        public class Manejador : IRequestHandler<LibreriaUnica, LibreriaDto>
        {
            public readonly IMapper _mapper;
            public Manejador(IMapper mapper)
            {
                _mapper = mapper;
            }
            public async Task<LibreriaDto> Handle(LibreriaUnica request, CancellationToken cancellationToken)
            {
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = "Gvh9e1qdp6zG2yJ9BNo3eTpWD2j6XvlUCnVuIX17",
                    BasePath = "https://tiendaservicios-e08e6-default-rtdb.firebaseio.com/"
                };
                FirebaseClient _client = new FireSharp.FirebaseClient(config);

                FirebaseResponse response = _client.Get($"Librerias/{request.Id}");
                dynamic data = JsonConvert.DeserializeObject<Libreria>(response.Body);
                if (data == null)
                {
                    throw new Exception("No se encuentra el autor");
                }
                //var libreria = JsonConvert.DeserializeObject<Libreria>(((JArray)data).ToString());
                var libreriaDto = _mapper.Map<Libreria, LibreriaDto>(data);
                return libreriaDto;

            }
        }
    }
}
