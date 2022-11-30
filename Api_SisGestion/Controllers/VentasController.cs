using Microsoft.AspNetCore.Mvc;
using Api_SisGestion.Repositories;
using System.Data.SqlClient;
using Api_SisGestion.Models;

namespace Api_SisGestion.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VentasController : Controller
    {
        [HttpGet("list")]
        public ActionResult<List<Venta>> GetVentas()
        {
            dynamic conn = DBConnect.ConnDB();

            if (conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(conn);
            }
            else
            {
                List<Venta> lista = new();
                try
                {
                    using (SqlCommand cmd = new("SELECT a.*, concat(u.Apellido,' ',u.Nombre) as Usuario " +
                                                "FROM Venta a " +
                                                "inner join Usuario u on u.Id = a.IdUsuario " +
                                                "order by 2", conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = new(int.Parse(reader["Id"].ToString()!),
                                                        reader["Comentarios"].ToString()!.ToUpper()!,
                                                        int.Parse(reader["IdUsuario"].ToString()!),
                                                        reader["Usuario"].ToString()!.ToUpper()!);
                                lista.Add(venta);
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
