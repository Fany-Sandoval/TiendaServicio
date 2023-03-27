using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading;
using System;
using MediatR;
using AutoMapper;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;
using TiendaServicios.Api.Librerias.Modelo;
using Newtonsoft.Json.Linq;

namespace TiendaServicios.Api.Librerias.Aplicacion
{
    public class Consulta
    {
        public class ListaLibreria : IRequest<List<LibreriaDto>>
        {

        }

        public class Manejador : IRequestHandler<ListaLibreria, List<LibreriaDto>>
        {
            private readonly IMapper _mapper;
            public Manejador(IMapper mapper)
            {
                _mapper = mapper;
            }
            public async Task<List<LibreriaDto>> Handle(ListaLibreria request, CancellationToken cancellationToken)
            {
                IFirebaseConfig config = new FirebaseConfig
                {
                    AuthSecret = "Gvh9e1qdp6zG2yJ9BNo3eTpWD2j6XvlUCnVuIX17",
                    BasePath = "https://tiendaservicios-e08e6-default-rtdb.firebaseio.com/"
                };

                FirebaseClient _client = new FireSharp.FirebaseClient(config); ;
                FirebaseResponse response = _client.Get("Librerias");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                var librerias = new List<Libreria>();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        librerias.Add(JsonConvert.DeserializeObject<Libreria>(((JProperty)item).Value.ToString()));
                    }
                }

                var libreriasDto = _mapper.Map<List<Libreria>, List<LibreriaDto>>(librerias);
                Console.WriteLine(libreriasDto);
                return libreriasDto;
            }
        }
    }
}
