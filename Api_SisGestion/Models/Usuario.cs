using System;

namespace Api_SisGestion.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contrasenia { get; set; }
        public string? Mail { get; set; }
        public byte Activo { get; set; }

        public Usuario(int id, string nombre, string apellido, string nombreUsuario, string contrasenia, string mail, byte activo)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
            Mail = mail; 
            Activo = activo;

        }

        public Usuario() {
            Id = 0;
            Nombre = null;
            Apellido = null;
            NombreUsuario = null;
            Contrasenia = null;
            Mail = null;
            Activo = 1;
        }
    }
}
