using Microsoft.AspNetCore.Mvc;
using Api_SisGestion.Repositories;
using Api_SisGestion.Models;

namespace Api_SisGestion.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductosVendidosController : Controller
    {
        [HttpGet("list/{idventa}")] /* Devuelve una lista de TODOS las items de una venta, o NotFound si no hay ninguno */
        public ActionResult<List<ProductoVendido>> GetProdVendidos(int idventa)
        {
            ProductoVendidoRepository reposiProdVend = new(DBConnect.ConnDB());

            if (reposiProdVend.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiProdVend.conn);
            }
            else
            {
                try
                {
                    List<ProductoVendido> lista = reposiProdVend.ProdVendidoList(idventa);

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
                    reposiProdVend.conn.Close();
                }
            }
        }

        [HttpPost("add")] /* Agrega un item a una venta y descuenta del stock: si se insertó, devuelve el id del item insertado  */
        public ActionResult<string> AddVenta([FromBody] ProductoVendido data)
        {
            ProductoVendidoRepository reposiProdVend = new(DBConnect.ConnDB());

            if (reposiProdVend.conn.GetType().ToString() == "System.String")
            {
                return ValidationProblem(reposiProdVend.conn);
            }
            else
            {
                try
                {
                    object column = reposiProdVend.ProdVendidoAdd(data);

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
                    reposiProdVend.conn.Close();
                }
            }
        }
        /* ============================================================================================================================= */
        /* [HttpPut("update")] => no se incluye método update en este caso, porque es mas sencillo eliminar un item y volverlo a agregar */
        /* ============================================================================================================================= */


        [HttpDelete("delete")]/* realiza una baja de un item en una venta y reintegra stock: si no pudo hacer delete devuelve 0, sino el ID dado de baja */
        public ActionResult<string> DeleteVenta([FromBody] int Id)
        {
            ProductoVendidoRepository reposiProdVend = new(DBConnect.ConnDB());

            if (reposiProdVend.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiProdVend.conn);
            }
            else
            {
                try
                {
                    int filas = reposiProdVend.ProdVendidoDelete(Id);

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
                    reposiProdVend.conn.Close();
                }
            }
        }

    }
}
