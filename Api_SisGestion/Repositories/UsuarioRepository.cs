using Api_SisGestion.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Api_SisGestion.Repositories
{
    public class UsuarioRepository
    {
        public dynamic conn { get; set; }
        public UsuarioRepository(dynamic conn)
        {
            this.conn = conn;
        }

        public List<Usuario> UsuariosList()
        {
            List<Usuario> lista = new();

            using (SqlCommand cmd = new("SELECT * FROM Usuario", conn))
            {

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lista.Add(LeerDatos(reader,false));
                    }
                }
            }

            return lista;
        }

        public Usuario UsuarioGet(int id)
        {
            Usuario usuario = new();

            using (SqlCommand cmd = new("SELECT * FROM Usuario where Id = @id ", conn))
            {


                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    usuario = LeerDatos(reader, false);


                }
            }
            return usuario;

        }

        public object UsuarioAdd(Usuario data)
        {
            using (SqlCommand cmd = new("Insert into Usuario ([Nombre],[Apellido],[NombreUsuario],[Contraseña],[Mail],[Activo]) " +
                                                "Values (@nombre,@apellido,@nombreUsuario,@contrasenia,@mail,@activo); select @@IDENTITY; ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = data.Nombre!.ToUpper() });
                cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = data.Apellido!.ToUpper() });
                cmd.Parameters.Add(new SqlParameter("nombreusuario", SqlDbType.VarChar) { Value = data.NombreUsuario!.ToUpper() });
                cmd.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar) { Value = data.Contrasenia });
                cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = data.Mail!.ToUpper() });
                cmd.Parameters.Add(new SqlParameter("activo", SqlDbType.Int) { Value = 1 }); //se agrega activo

                return cmd.ExecuteScalar();
            }

        }

        public int UsuarioUpdate(Usuario data)
        {
            using (SqlCommand cmd = new("SELECT * FROM Usuario where Id = @id ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = data.Id });

                Usuario usuario = new();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    usuario = LeerDatos(reader, true);

                    reader.Close();

                    using (SqlCommand cmd2 = new("Update  Usuario " +
                                       "set [Nombre]=@nombre,[Apellido]=@apellido,[NombreUsuario]=@nombreUsuario,[Mail]=@mail, [Activo]=@activo, [Contraseña]=@contrasenia  " +
                                        "where Id = @id ", conn))
                    {
                        cmd2.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = data.Id });

                        if (usuario.Nombre != data.Nombre)
                        {
                            cmd2.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = data.Nombre!.ToUpper() });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre!.ToUpper() });
                        }

                        if (usuario.Apellido != data.Apellido)
                        {
                            cmd2.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = data.Apellido!.ToUpper() });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido!.ToUpper() });
                        }

                        if (usuario.NombreUsuario != data.NombreUsuario)
                        {
                            cmd2.Parameters.Add(new SqlParameter("nombreusuario", SqlDbType.VarChar) { Value = data.NombreUsuario!.ToUpper() });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("nombreusuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario!.ToUpper() });
                        }

                        if (usuario.Mail != data.Mail)
                        {
                            cmd2.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = data.Mail!.ToUpper() });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail!.ToUpper() });
                        }

                        if (usuario.Contrasenia != data.Contrasenia)
                        {
                            if ((data.Contrasenia == null) || (data.Contrasenia == "")){
                                cmd2.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar) { Value = usuario.Contrasenia! });
                            }
                            else
                            {
                                cmd2.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar) { Value = data.Contrasenia! });
                            }
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar) { Value = usuario.Contrasenia! });
                        }

                        if (usuario.Activo != data.Activo)
                        {
                            cmd2.Parameters.Add(new SqlParameter("activo", SqlDbType.VarChar) { Value = data.Activo! });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("activo", SqlDbType.VarChar) { Value = usuario.Activo! });
                        }

                        return cmd2.ExecuteNonQuery();
                    }
                }
                else
                {
                    return -1;
                }
            }
        }

        public int UsuarioPassword(Usuario data)
        {
            using (SqlCommand cmd2 = new("Update  Usuario " +
                                       "set [Contraseña]=@contrasenia " +
                                        "where Id = @id ", conn))
            {
                cmd2.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = int.Parse(data.Id.ToString()) });
                cmd2.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar) { Value = data.Contrasenia!.ToString() });

                return cmd2.ExecuteNonQuery();
            }
        }

        public Usuario UsuarioLogin(Usuario data)
        {
            Usuario usuario = new();

            using (SqlCommand cmd = new("SELECT Id, Nombre, Apellido, Mail, NombreUsuario, Activo FROM Usuario " +
                                                "where upper(Mail) = @mail " +
                                                "and Contraseña = @pass " +
                                                "and Activo = 1 ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = data.Mail!.ToUpper() });
                cmd.Parameters.Add(new SqlParameter("pass", SqlDbType.VarChar) { Value = data.Contrasenia });

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    usuario = LeerDatos(reader, false);
                }
            }
            return usuario;
        }

        public int UsuarioDelete(int Id)
        {
            using (SqlCommand cmd = new("update Usuario set Activo=0 where Id = @Id and Activo = 1 ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = Id });
                return cmd.ExecuteNonQuery();
            }
        }

        private Usuario LeerDatos(SqlDataReader reader, bool completo)
        {
            if (completo)
            {
                return new(int.Parse(reader["Id"].ToString()!),
                                                reader["Nombre"].ToString()!.ToUpper()!,
                                                reader["Apellido"].ToString()!.ToUpper()!,
                                                reader["NombreUsuario"].ToString()!.ToUpper()!,
                                                reader["Contraseña"].ToString()!,
                                                reader["Mail"].ToString()!.ToUpper()!,
                                                byte.Parse(reader["Activo"].ToString()!));
            }
            else
            {
                return new(int.Parse(reader["Id"].ToString()!),
                                               reader["Nombre"].ToString()!.ToUpper()!,
                                               reader["Apellido"].ToString()!.ToUpper()!,
                                               reader["NombreUsuario"].ToString()!.ToUpper()!,
                                               null!,
                                               reader["Mail"].ToString()!.ToUpper()!,
                                               byte.Parse(reader["Activo"].ToString()!));
            }
        }
    }
}
