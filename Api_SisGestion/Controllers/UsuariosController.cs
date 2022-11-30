using Microsoft.AspNetCore.Mvc;
using Api_SisGestion.Repositories;
using System.Data.SqlClient;
using Api_SisGestion.Models;
using System.Data;

namespace Api_SisGestion.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController : Controller
    {
        [HttpGet("list")]
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
                                                        reader["Mail"].ToString()!.ToUpper()!);
                                lista.Add(usuario);
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

        [HttpPost("login/{u}/{p}")]
        public IActionResult PostLogin(string u, string p)
        {
            dynamic conn = DBConnect.ConnDB();

            if (conn.GetType().ToString() == "System.String")
            {

                return ValidationProblem(conn);
            }
            else
            {
                List<Usuario> lista = new();

                try
                {
                    using (SqlCommand cmd = new("SELECT Id, Nombre, Apellido, Mail FROM Usuario "+
                                                "where upper(NombreUsuario) = @user " +
                                                "and Contraseña = @pass", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("user", SqlDbType.VarChar) { Value = u.ToUpper() });
                        cmd.Parameters.Add(new SqlParameter("pass", SqlDbType.VarChar) { Value = p });

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new(int.Parse(reader["Id"].ToString()!),
                                                        reader["Nombre"].ToString()!.ToUpper()!,
                                                        reader["Apellido"].ToString()!.ToUpper()!,
                                                        reader["Mail"].ToString()!.ToUpper()!);
                                lista.Add(usuario);
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
