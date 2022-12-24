using Api_SisGestion.Models;
using System.Data;
using System.Data.SqlClient;

namespace Api_SisGestion.Repositories
{
    public class ProductoRepository
    {
        public dynamic conn { get; set; }
        public ProductoRepository(dynamic conn)
        {
            this.conn = conn;
        }

        public List<Producto> ProductoList() {
            List<Producto> lista = new();

            using (SqlCommand cmd = new("Select a.*, concat(b.Nombre,' ',b.Apellido) as ApeNomUsuario FROM Producto a inner join Usuario b on b.Id = a.IdUsuario  ", conn))
            {

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lista.Add(LeerDatos(reader));
                    }
                }
            }

            return lista;
        }

        public Producto ProductoGet(int id) {
            Producto producto = new();

            using (SqlCommand cmd = new("Select a.*, concat(b.Nombre,' ',b.Apellido) as ApeNomUsuario FROM Producto a inner join Usuario b on b.Id = a.IdUsuario where a.Id = @id ", conn))
            {


                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    producto = LeerDatos(reader);


                }
            }
            return producto;
        }

        public object ProductoAdd(Producto data) {
            using (SqlCommand cmd = new("Insert into Producto ([Descripciones],[Costo],[PrecioVenta],[Stock],[IdUsuario],[Activo]) " +
                                                "Values (@descripciones,@costo,@precioventa,@stock,@idusuario,@activo); select @@IDENTITY; ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("descripciones", SqlDbType.VarChar) { Value = data.Descripciones!.ToUpper() });
                cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = float.Parse(data.Costo.ToString()) });
                cmd.Parameters.Add(new SqlParameter("precioventa", SqlDbType.Float) { Value = float.Parse(data.PrecioVenta.ToString()) });
                cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = int.Parse(data.Stock.ToString()) });
                cmd.Parameters.Add(new SqlParameter("idusuario", SqlDbType.Int) { Value = int.Parse(data.IdUsuario.ToString()) });
                cmd.Parameters.Add(new SqlParameter("activo", SqlDbType.Int) { Value = int.Parse(data.Activo.ToString()) });

                return cmd.ExecuteScalar();
            }
        }

        public int ProductoUpdate(Producto data) {
            using (SqlCommand cmd = new("SELECT *, '' as ApeNomUsuario FROM Producto where Id = @id ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = data.Id });

                Producto producto = new();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    producto = LeerDatos(reader);

                    reader.Close();

                    using (SqlCommand cmd2 = new("Update  Producto " +
                                       "SET [Descripciones] = @descripciones,[Costo] = @costo,[PrecioVenta] = @precioventa, [Stock] = @stock,[IdUsuario] = @idusuario, [Activo] = @activo " +
                                        "where Id = @id ", conn))
                    {
                        cmd2.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = data.Id });

                        if (producto.Descripciones != data.Descripciones)
                        {
                            cmd2.Parameters.Add(new SqlParameter("descripciones", SqlDbType.VarChar) { Value = data.Descripciones!.ToUpper() });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones! });
                        }

                        if (producto.Costo != data.Costo)
                        {
                            cmd2.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = data.Costo! });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = producto.Costo! });
                        }

                        if (producto.PrecioVenta != data.PrecioVenta)
                        {
                            cmd2.Parameters.Add(new SqlParameter("precioventa", SqlDbType.Float) { Value = data.PrecioVenta! });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("precioventa", SqlDbType.Float) { Value = producto.PrecioVenta!});
                        }

                        if (producto.Stock != data.Stock)
                        {
                            cmd2.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = data.Stock! });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = producto.Stock! });
                        }

                        if (producto.IdUsuario != data.IdUsuario)
                        {
                            cmd2.Parameters.Add(new SqlParameter("idusuario", SqlDbType.Int) { Value = data.IdUsuario! });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("idusuario", SqlDbType.Int) { Value = producto.IdUsuario! });
                        }

                        if (producto.Activo != data.Activo)
                        {
                            cmd2.Parameters.Add(new SqlParameter("activo", SqlDbType.Int) { Value = data.Activo! });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("activo", SqlDbType.VarChar) { Value = producto.Activo! });
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

        public int ProductoDelete(int Id) {
            using (SqlCommand cmd = new("update Producto set Activo=0 where Id = @id and Activo = 1 ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = Id });
                return cmd.ExecuteNonQuery();
            }
        }

        private Producto LeerDatos(SqlDataReader reader)
        {
            return new(int.Parse(reader["Id"].ToString()!),
                                        reader["Descripciones"].ToString()!.ToUpper()!,
                                        double.Parse(reader["Costo"].ToString()!),
                                        double.Parse(reader["PrecioVenta"].ToString()!),
                                        int.Parse(reader["Stock"].ToString()!),
                                        int.Parse(reader["IdUsuario"].ToString()!),
                                        byte.Parse(reader["Activo"].ToString()!),
                                        reader["ApeNomUsuario"].ToString()!.ToUpper()!);

        }
    }
}
