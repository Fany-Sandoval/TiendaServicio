using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicio.CarritodeCompra.RemoteInterface;
using TiendaServicio.CarritodeCompra.RemoteModel;

namespace TiendaServicio.CarritodeCompra.RemoteServices
{
    public class LibrosService: ILibroService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibrosService> logger;

        public LibrosService(IHttpClientFactory _httpclient, ILogger<LibrosService> _logger)
        {
            _httpClient = _httpclient;
            logger= _logger;
        }
        public async Task <(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                var cliente = _httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/Libro/{LibroId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido= await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() {  PropertyNameCaseInsensitive= true };
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido,options);
                    return ( true,resultado, null );
                }
                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {

                logger.LogError(ex.ToString());
                return( false, null, ex.Message );
            }
        }

    }
}
