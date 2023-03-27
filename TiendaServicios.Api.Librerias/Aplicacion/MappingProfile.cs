using AutoMapper;
using TiendaServicios.Api.Librerias.Modelo;

namespace TiendaServicios.Api.Librerias.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Libreria, LibreriaDto>();
        }
    }
}
