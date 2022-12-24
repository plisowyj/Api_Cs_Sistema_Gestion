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
        [HttpGet("list")] /* Devuelve una lista de TODOS los productos, o NotFound si no hay ninguno */
        public ActionResult<List<Producto>> GetProductos()
        {
            ProductoRepository reposiProducto = new(DBConnect.ConnDB());

            if (reposiProducto.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiProducto.conn);
            }
            else
            {
                try
                {
                    List<Producto> lista = reposiProducto.ProductoList();

                    if (lista.Count > 0)
                    {
                        return Ok(lista);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return ValidationProblem(ex.Message);
                }
                finally
                {
                    reposiProducto.conn.Close();
                }
            }
        }

        [HttpGet("get/{id}")] /* Devuelve un producto segun el parametro 'id', o NotFound si no se encontró */
        public ActionResult<Producto> GetProducto(int id)
        {
            ProductoRepository reposiProducto = new(DBConnect.ConnDB());

            if (reposiProducto.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiProducto.conn);
            }
            else
            {
                try
                {
                    Producto producto = reposiProducto.ProductoGet(id);

                    if (producto.Id > 0)
                    {
                        return Ok(producto);
                    }
                    else
                    {
                        return NotFound();
                    }

                }
                catch (Exception ex)
                {
                    return ValidationProblem(ex.Message);
                }
                finally
                {
                    reposiProducto.conn.Close();
                }
            }
        }

        [HttpPost("add")] /* Agrega un nuevo producto: si se insertó, devuelve el id del producto insertado */
        public ActionResult<string> AddProducto([FromBody] Producto data)
        {
            ProductoRepository reposiProducto = new(DBConnect.ConnDB());

            if (reposiProducto.conn.GetType().ToString() == "System.String")
            {
                return ValidationProblem(reposiProducto.conn);
            }
            else
            {
                try
                {
                    object column = reposiProducto.ProductoAdd(data);

                    if (column != null)
                    {

                        return Ok(column.ToString());
                    }
                    else
                    {
                        return ValidationProblem("No se recuperó un Id.");
                    }
                }
                catch (Exception ex)
                {
                    return ValidationProblem(ex.Message);
                }
                finally
                {
                    reposiProducto.conn.Close();
                }
            }
        }

        [HttpPut("update")] /* actualiza un producto: si se actualizó, devuelve el id del producto actualizado */
        public ActionResult<string> UpdateProducto([FromBody] Producto data)
        {
            ProductoRepository reposiProducto = new(DBConnect.ConnDB());

            if (reposiProducto.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiProducto.conn);
            }
            else
            {
                try
                {
                    int filas = reposiProducto.ProductoUpdate(data);
                    if (filas > 0)
                    {
                        return Ok(data.Id.ToString());
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return ValidationProblem(ex.Message);
                }
                finally
                {
                    reposiProducto.conn.Close();
                }
            }
        }

        [HttpDelete("delete")]/* realiza una baja lógica de un producto si existe en la DB: devuelve confirmacion ELIMINADO, o NotFound si no se hizo la baja. */
        public ActionResult<string> DeleteProducto([FromBody]int Id)
        {
            ProductoRepository reposiProducto = new(DBConnect.ConnDB());

            if (reposiProducto.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiProducto.conn);
            }
            else
            {
                try
                {   //se hace una baja lógica
                    int filas = reposiProducto.ProductoDelete(Id);

                    if (filas > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return ValidationProblem(ex.Message);
                }
                finally
                {
                    reposiProducto.conn.Close();
                }
            }
        }
    }
}
