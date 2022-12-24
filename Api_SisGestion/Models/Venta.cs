namespace Api_SisGestion.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public string? Comentarios { get; set; }
        public int IdUsuario { get; set; }
        public string? ApeNomUsuario { get; set; }
        public List<ProductoVendido>? VentaProducto { get; set; }

        public Venta(int id, string comentarios, int idUsuario, List<ProductoVendido> ventaProducto, string apeNomUsuario)
        {
            Id = id;
            Comentarios = comentarios;
            IdUsuario = idUsuario;
            VentaProducto = ventaProducto;
            ApeNomUsuario = apeNomUsuario;
        }

        public Venta()
        {
            Id=0;
            Comentarios = null;
            IdUsuario = 0;
            VentaProducto = null;
            ApeNomUsuario = null;
        }
    }
}
