using Microsoft.AspNetCore.Mvc;
using Api_SisGestion.Repositories;
using System.Data.SqlClient;
using Api_SisGestion.Models;

namespace Api_SisGestion.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductosVendidosController : Controller
    {
        [HttpGet("list")]
        public ActionResult<List<ProductoVendido>> GetProductosVendidos()
        {
            dynamic conn = DBConnect.ConnDB();

            if (conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(conn);
            }
            else
            {
                List<ProductoVendido> lista = new();
                try
                {
                    using (SqlCommand cmd = new("SELECT v.Comentarios as Venta, b.Descripciones as Producto, a.* "+
                                                "FROM ProductoVendido a "+
                                                "inner join Producto b on b.Id = a.IdProducto "+
                                                "inner join Venta v on v.Id = a.IdVenta "+
                                                "order by 1,2", conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido productovendido = new(int.Parse(reader["Id"].ToString()!),
                                                        int.Parse(reader["IdProducto"].ToString()!),
                                                        int.Parse(reader["Stock"].ToString()!),
                                                        int.Parse(reader["IdVenta"].ToString()!),
                                                        reader["Venta"].ToString()!.ToUpper()!,
                                                        reader["Producto"].ToString()!.ToUpper()!);
                                lista.Add(productovendido);
                            }
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    conn.Close();
                    return Ok(lista);
                }
                catch (Exception ex)
                {
                    return ValidationProblem(ex.Message);
                }
            }
        }
    }
}
