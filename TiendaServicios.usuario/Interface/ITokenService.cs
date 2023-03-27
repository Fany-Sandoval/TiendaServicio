using TiendaServicios.usuario.Modelo;

namespace TiendaServicios.usuario.Interface
{
    public interface ITokenService
    {

        //Metodo creacion de token en la implemnetacion
        string CreateToken(Users user);
    }
}
