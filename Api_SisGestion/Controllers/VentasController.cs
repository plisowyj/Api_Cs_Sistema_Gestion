using Microsoft.AspNetCore.Mvc;
using Api_SisGestion.Repositories;
using Api_SisGestion.Models;

namespace Api_SisGestion.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VentasController : Controller
    {
        [HttpGet("list")] /* Devuelve una lista de TODOS las ventas (con su detalle), o NotFound si no hay ninguno */
        public ActionResult<List<Venta>> GetVentas()
        {
            VentaRepository reposiVenta = new(DBConnect.ConnDB());

            if (reposiVenta.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiVenta.conn);
            }
            else
            {
                try
                {
                    List<Venta> lista = reposiVenta.VentaList();

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
                    reposiVenta.conn.Close();
                }
            }
        }

        [HttpGet("get/{id}")] /* Devuelve una venta segun el parametro 'id', o NotFound si no se encontró */
        public ActionResult<Venta> GetVenta(int id)
        {
            VentaRepository reposiVenta = new(DBConnect.ConnDB());

            if (reposiVenta.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiVenta.conn);
            }
            else
            {
                try
                {
                    Venta venta= reposiVenta.VentaGet(id);

                    if (venta.Id > 0)
                    {
                        return Ok(venta);
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
                    reposiVenta.conn.Close();
                }
            }
        }

        [HttpPost("add")] /* Agrega una nueva venta(sin items detalles): si se insertó, devuelve el id de la venta insertada  */
        public ActionResult<string> AddVenta([FromBody] Venta data)
        {
            VentaRepository reposiVenta = new(DBConnect.ConnDB());

            if (reposiVenta.conn.GetType().ToString() == "System.String")
            {
                return ValidationProblem(reposiVenta.conn);
            }
            else
            {
                try
                {
                    object column = reposiVenta.VentaAdd(data);

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
                    reposiVenta.conn.Close();
                }
            }
        }

        [HttpPut("update")] /* actualiza una descripcion de venta: si se actualizó, devuelve el id de la Venta actualizada */
        public ActionResult<string> UpdateVenta([FromBody] Venta data)
        {
            VentaRepository reposiVenta = new(DBConnect.ConnDB());

            if (reposiVenta.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiVenta.conn);
            }
            else
            {
                try
                {
                    int filas = reposiVenta.VentaUpdate(data);
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
                    reposiVenta.conn.Close();
                }
            }
        }

        [HttpDelete("delete")]/* realiza una baja de una venta y sus detalles(reintegrando stocks): si no pudo hacer delete devuelve 0, sino el ID dado de baja de la venta */
        public ActionResult<string> DeleteVenta([FromBody] int Id)
        {
            VentaRepository reposiVenta = new(DBConnect.ConnDB());

            if (reposiVenta.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiVenta.conn);
            }
            else
            {
                try
                {   int filas = reposiVenta.VentaDelete(Id);

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
                    reposiVenta.conn.Close();
                }
            }
        }
    }
}
