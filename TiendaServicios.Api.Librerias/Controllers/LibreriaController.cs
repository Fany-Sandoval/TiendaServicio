using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaServicios.Api.Librerias.Aplicacion;

namespace TiendaServicios.Api.Librerias.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class LibreriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LibreriaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibreriaDto>>> GetLibrerias()
        {
            return await _mediator.Send(new Consulta.ListaLibreria());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibreriaDto>> GetAutorLibro(string id)
        {
            return await _mediator.Send(new ConsultarFiltro.LibreriaUnica { Id = id });
        }
    }
}
