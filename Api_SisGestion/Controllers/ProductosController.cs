using Microsoft.AspNetCore.Mvc;
using Api_SisGestion.Repositories;
using System.Data.SqlClient;
using Api_SisGestion.Models;

namespace Api_SisGestion.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductosController : Controller
    {
        [HttpGet("list")]
        public ActionResult<List<Producto>> GetProductos()
        {
            dynamic conn = DBConnect.ConnDB();

            if (conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(conn);
            }
            else
            {
                List<Producto> lista = new();
                try
                {
                    using (SqlCommand cmd = new("SELECT * FROM Producto", conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = new(int.Parse(reader["Id"].ToString()!),
                                                        reader["Descripciones"].ToString()!.ToUpper()!,
                                                        double.Parse(reader["Costo"].ToString()!),
                                                        double.Parse(reader["PrecioVenta"].ToString()!),
                                                        int.Parse(reader["Stock"].ToString()!),
                                                        int.Parse(reader["IdUsuario"].ToString()!));
                                lista.Add(producto);
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
