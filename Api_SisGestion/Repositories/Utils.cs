namespace Api_SisGestion.Repositories
{
    public class Utils
    {
        public class LoginData
        {
            public string? U { get; set; }
            public string? P { get; set; }
        }

        public class UDatos
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string? Apellido { get; set; }
            public string? Mail { get; set; }
            public string NombreUsuario { get; set; }

            public UDatos(int id, string? nombre, string? apellido, string? mail, string nombreUsuario)
            {
                Id = id;
                Nombre = nombre;
                Apellido = apellido;
                Mail = mail;
                NombreUsuario = nombreUsuario;
            }
        }

        public class UAdd
        {
            public string? Nombre { get; set; }
            public string? Apellido { get; set; }
            public string NombreUsuario { get; set; }
            public string Contrasenia { get; set; }
            public string Mail { get; set; }
            public byte Activo { get; set; }

            public UAdd(string nombre, string apellido, string nombreUsuario, string contrasenia, string mail, byte activo)
            {
                Nombre = nombre;
                Apellido = apellido;
                NombreUsuario = nombreUsuario;
                Contrasenia = contrasenia;
                Mail = mail;
                Activo = activo;

            }
        }
    }
}
