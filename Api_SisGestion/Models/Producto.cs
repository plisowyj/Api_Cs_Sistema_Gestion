namespace Api_SisGestion.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string? Descripciones { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }
        public byte Activo { get; set; }
        public string? ApeNomUsuario { get; set; }

        public Producto(int id, string descripciones, double costo, double precioVenta, int stock, int idUsuario, byte activo, string apeNomUsuario)
        {
            Id = id;
            Descripciones = descripciones;
            Costo = costo;
            PrecioVenta = precioVenta;
            Stock = stock;
            IdUsuario = idUsuario;
            Activo = activo;
            ApeNomUsuario = apeNomUsuario;
        }

        public Producto() 
        {
            Id = 0;
            Descripciones = null;
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;
            Activo = 0;
            ApeNomUsuario=null;
        }
    }
}
