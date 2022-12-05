using Microsoft.AspNetCore.Mvc;
using Api_SisGestion.Repositories;
using System.Data.SqlClient;
using Api_SisGestion.Models;
using System.Data;
using static Api_SisGestion.Repositories.Utils;

namespace Api_SisGestion.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController : Controller
    {

        [HttpGet("list")] /* Devuelve una lista de TODOS los usuarios, o NotFound si no hay ninguno */
        public  ActionResult<List<Usuario>> GetUsuarios()
        {
            dynamic conn = DBConnect.ConnDB();

            if (conn.GetType().ToString() == "System.String") {
         
                return ValidationProblem(conn); 
            }
            else {
                List<Usuario> lista = new();
                try
                {
                    using (SqlCommand cmd = new("SELECT * FROM Usuario", conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new(int.Parse(reader["Id"].ToString()!),
                                                        reader["Nombre"].ToString()!.ToUpper()!,
                                                        reader["Apellido"].ToString()!.ToUpper()!,
                                                        reader["NombreUsuario"].ToString()!.ToUpper()!,
                                                        reader["Contraseña"].ToString()!.ToUpper()!,
                                                        reader["Mail"].ToString()!.ToUpper()!,
                                                        byte.Parse(reader["Activo"].ToString()!)); 
                                lista.Add(usuario);
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

        [HttpGet("get/{id}")] /* Devuelve un usuario segun el parametro 'id', o NotFound si no se encontró */
        public ActionResult<Usuario> GetUsuario(int id)
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
                    Usuario usuario=null!;
                    using (SqlCommand cmd = new("SELECT * FROM Usuario where Id = @id ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("id", SqlDbType.VarChar) { Value = id });

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                usuario = new(int.Parse(reader["Id"].ToString()!),
                                                        reader["Nombre"].ToString()!.ToUpper()!,
                                                        reader["Apellido"].ToString()!.ToUpper()!,
                                                        reader["NombreUsuario"].ToString()!.ToUpper()!,
                                                        null!,
                                                        reader["Mail"].ToString()!.ToUpper()!,
                                                        byte.Parse(reader["Activo"].ToString()!));
                                
                            }
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return Ok(usuario);

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

        [HttpPost("add")] /* Agrega un nuevo usuario: si se insertó, devuelve el id del Usuario insertado */
        public ActionResult AddUsuario([FromBody] UAdd data)
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
                    using (SqlCommand cmd = new("Insert into Usuario ([Nombre],[Apellido],[NombreUsuario],[Contraseña],[Mail],[Activo]) "+
                                                "Values (@nombre,@apellido,@nombreUsuario,@contrasenia,@mail,@activo); select @@IDENTITY; ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = data.Nombre!.ToUpper() });
                        cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = data.Apellido!.ToUpper() });
                        cmd.Parameters.Add(new SqlParameter("nombreusuario", SqlDbType.VarChar) { Value = data.NombreUsuario!.ToUpper() });
                        cmd.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar) { Value = data.Contrasenia });
                        cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = data.Mail!.ToUpper() });
                        cmd.Parameters.Add(new SqlParameter("activo", SqlDbType.Int) { Value = int.Parse(data.Activo.ToString())});

                        object column = cmd.ExecuteScalar();
                        if (column != null)
                        {
                            
                            return Ok(column.ToString());
                        }
                        else
                        {
                            return ValidationProblem("No se recuperó Id.");
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

        [HttpPut("update")] /* actualiza un usuario: si se insertó, devuelve el id del Usuario actualizado */
        public ActionResult UpdateUsuario([FromBody] UDatos data)
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
                    using (SqlCommand cmd = new("Update  Usuario "+
                                               "set [Nombre]=@nombre,[Apellido]=@apellido,[NombreUsuario]=@nombreUsuario,[Mail]=@mail  " +
                                                "where Id = @id ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = data.Id });
                        cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = data.Nombre!.ToUpper() });
                        cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = data.Apellido!.ToUpper() });
                        cmd.Parameters.Add(new SqlParameter("nombreusuario", SqlDbType.VarChar) { Value = data.NombreUsuario!.ToUpper() });
                        cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = data.Mail!.ToUpper() });
                        
                        int filas= cmd.ExecuteNonQuery();
                        if (filas > 0)
                        {

                            return Ok(data.Id.ToString());
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

        [HttpPost("login")] /* Devuelve datos del usuario no críticos, si el NombreUsuario y contraseña encuentran coincidencia en la DB; sino retorna NotFound */
        public ActionResult<UDatos> Login([FromBody] LoginData data)
        {
            dynamic conn = DBConnect.ConnDB();

            if (conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(conn);
            }
            else
            {
                UDatos login = null!;

                try
                {
                    using (SqlCommand cmd = new("SELECT Id, Nombre, Apellido, Mail, NombreUsuario FROM Usuario " +
                                                "where upper(NombreUsuario) = @user " +
                                                "and Contraseña = @pass "+
                                                "and Activo = 1 ", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("user", SqlDbType.VarChar) { Value = data.U!.ToUpper() });
                        cmd.Parameters.Add(new SqlParameter("pass", SqlDbType.VarChar) { Value = data.P });

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                             login = new(int.Parse(reader["Id"].ToString()!),
                                                        reader["Nombre"].ToString()!.ToUpper()!,
                                                        reader["Apellido"].ToString()!.ToUpper()!,
                                                        reader["Mail"].ToString()!.ToUpper()!,
                                                        reader["NombreUsuario"].ToString()!.ToUpper()!);
                                                        
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    
                    return Ok(login);
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

        [HttpDelete("delete")] /* realiza una baja lógica de un usuario si existe en la DB: devuelve confirmacion ELIMINADO o NotFound. */
        public ActionResult DeleteUsuario([FromBody] int Id)
        {
            dynamic conn = DBConnect.ConnDB();

            if (conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(conn);
            }
            else
            {
                try
                {   //se hace una baja lógica
                    using (SqlCommand cmd = new("update Usuario set Activo=0 where Id = @Id and Activo = 1 ", conn))
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
