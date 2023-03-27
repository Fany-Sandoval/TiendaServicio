using System;
using System.Threading.Tasks;
using TiendaServicio.CarritodeCompra.RemoteModel;

namespace TiendaServicio.CarritodeCompra.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId);
    }
}
