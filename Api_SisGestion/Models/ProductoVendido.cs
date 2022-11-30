namespace Api_SisGestion.Models
{
    public class ProductoVendido
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public int IdVenta { get; set; }
        public string Venta { get; set; }
        public string Producto { get; set; }

        public ProductoVendido(int id, int idProducto, int stock, int idVenta, string venta, string producto)
        {
            Id = id;
            IdProducto = idProducto;
            Stock = stock;
            IdVenta = idVenta;
            Venta = venta;
            Producto = producto;
        }
    }
}
