﻿using System.ComponentModel.DataAnnotations;

namespace TiendaServicios.usuario.Aplicacion
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
