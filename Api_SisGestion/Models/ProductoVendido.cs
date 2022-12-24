namespace Api_SisGestion.Models
{
    public class ProductoVendido
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }
        public string? ProductoDesc { get; set; }

        public ProductoVendido(int id, int stock, int idProducto, int idVenta, string productoDesc)
        {
            Id = id;
            Stock = stock;
            IdProducto = idProducto;
            IdVenta = idVenta;
            ProductoDesc = productoDesc;
        }

        public ProductoVendido()
        {
            Id = 0;
            Stock = 0;
            IdProducto = 0;
            IdVenta = 0;
            ProductoDesc = null;
        }
    }
}
