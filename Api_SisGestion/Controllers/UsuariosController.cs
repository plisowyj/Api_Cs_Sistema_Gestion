using Microsoft.AspNetCore.Mvc;
using Api_SisGestion.Repositories;
using Api_SisGestion.Models;
using Microsoft.AspNetCore.Cors;

namespace Api_SisGestion.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController : Controller
    {
        [HttpGet("list")] /* Devuelve una lista de TODOS los usuarios, o NotFound si no hay ninguno */
        public  ActionResult<List<Usuario>> GetUsuarios()
        {
            UsuarioRepository reposiUsuario = new(DBConnect.ConnDB());

            if (reposiUsuario.conn.GetType().ToString() == "System.String") {
         
                return ValidationProblem(reposiUsuario.conn); 
            }
            else {
                try
                {
                    List<Usuario> lista = reposiUsuario.UsuariosList();

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
                    reposiUsuario.conn.Close();
                }
            }
        }

        [HttpGet("get/{id}")] /* Devuelve un usuario segun el parametro 'id', o NotFound si no se encontró */
        public ActionResult<Usuario> GetUsuario(int id)
        {
            UsuarioRepository reposiUsuario = new(DBConnect.ConnDB());

            if (reposiUsuario.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiUsuario.conn);
            }
            else
            {
                try
                {
                    Usuario usuario = reposiUsuario.UsuarioGet(id);

                    if (usuario.Id > 0)
                    {
                        return Ok(usuario);
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
                    reposiUsuario.conn.Close();
                }
            }
        }

        [HttpPost("add")] /* Agrega un nuevo usuario: si se insertó, devuelve el id del Usuario insertado */
        public ActionResult<string> AddUsuario([FromBody] Usuario data)
        {
            UsuarioRepository reposiUsuario = new(DBConnect.ConnDB());

            if (reposiUsuario.conn.GetType().ToString() == "System.String")
            {
                return ValidationProblem(reposiUsuario.conn);
            }
            else
            {
                try
                {
                    object column = reposiUsuario.UsuarioAdd(data)!;

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
                    reposiUsuario.conn.Close();
                }
            }
        }

        [HttpPost("login")] /* Devuelve datos del usuario no críticos, si el NombreUsuario y contraseña encuentran coincidencia en la DB; sino retorna NotFound */
        public ActionResult<Usuario> Login([FromBody] Usuario data)
        {
            UsuarioRepository reposiUsuario = new(DBConnect.ConnDB());

            if (reposiUsuario.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiUsuario.conn);
            }
            else
            {
                try
                {
                    Usuario login = reposiUsuario.UsuarioLogin(data);

                    if (login.Id > 0)
                    {
                        return Ok(login);
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
                    reposiUsuario.conn.Close();
                }
            }
        }

        [HttpPut("update")] /* actualiza un usuario: si se actualizó, devuelve el id del Usuario actualizado */
        public ActionResult<string> UpdateUsuario([FromBody] Usuario data)
        {
            UsuarioRepository reposiUsuario = new(DBConnect.ConnDB());

            if (reposiUsuario.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiUsuario.conn);
            }
            else
            {
                try
                {
                    int filas = reposiUsuario.UsuarioUpdate(data);
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
                    reposiUsuario.conn.Close();
                }
            }
        }

        [HttpPut("updatepass")] /* actualiza un SÓLO la clave usuario: si se actualizó, devuelve el id del Usuario actualizado */
        public ActionResult<string> UpdateUsuarioPass([FromBody] Usuario data)
        {
            UsuarioRepository reposiUsuario = new(DBConnect.ConnDB());

            if (reposiUsuario.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiUsuario.conn);
            }
            else
            {
                try
                {
                    int filas = reposiUsuario.UsuarioPassword(data);
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
                    reposiUsuario.conn.Close();
                }
            }
        }

        [HttpDelete("delete")] /* realiza una baja lógica de un usuario si existe en la DB: devuelve confirmacion ELIMINADO o NotFound si no se hizo la baja. */
        public ActionResult<string> DeleteUsuario([FromBody] int Id)
        {
            UsuarioRepository reposiUsuario = new(DBConnect.ConnDB());

            if (reposiUsuario.conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(reposiUsuario.conn);
            }
            else
            {
                try
                {   //se hace una baja lógica
                    int filas = reposiUsuario.UsuarioDelete(Id);

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
                    reposiUsuario.conn.Close();
                }
            }
        }
    }
    
}
