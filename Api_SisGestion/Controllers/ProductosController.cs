using Microsoft.AspNetCore.Mvc;
using Api_SisGestion.Repositories;
using System.Data.SqlClient;
using Api_SisGestion.Models;
using System.Data;

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
                                Producto producto = LeerDatos(reader);
                                lista.Add(producto);
                            }
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return Ok(lista);
                }
                catch (Exception ex)
                {
                    return ValidationProblem(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        [HttpGet("get/{id}")]
        public ActionResult<List<Producto>> GetProducto(int id)
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
                    using (SqlCommand cmd = new("SELECT * FROM Producto where Id = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id});

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();

                            Producto producto = LeerDatos(reader);
                            lista.Add(producto);
                            
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return Ok(lista);
                }
                catch (Exception ex)
                {
                    return ValidationProblem(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private Producto LeerDatos(SqlDataReader reader)
        {
            return new(int.Parse(reader["Id"].ToString()!),
                                        reader["Descripciones"].ToString()!.ToUpper()!,
                                        double.Parse(reader["Costo"].ToString()!),
                                        double.Parse(reader["PrecioVenta"].ToString()!),
                                        int.Parse(reader["Stock"].ToString()!),
                                        int.Parse(reader["IdUsuario"].ToString()!));

        }

        [HttpDelete("delete")]
        public ActionResult DeleteProducto([FromBody]int Id)
        {
            dynamic conn = DBConnect.ConnDB();

            if (conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(conn);
            }
            else
            {                
                try
                {
                    using (SqlCommand cmd = new("Delete FROM Producto where Id = @Id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = Id });
                        int filas = cmd.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            return Ok("Eliminado");
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    return ValidationProblem(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
