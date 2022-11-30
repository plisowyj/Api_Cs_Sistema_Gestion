namespace Api_SisGestion.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }

        public Venta(int id, string comentarios, int idUsuario,string usuario)
        {
            Id = id;
            Comentarios = comentarios;
            IdUsuario = idUsuario;
            Usuario = usuario;
        }
    }
}
