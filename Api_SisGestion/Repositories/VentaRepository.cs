using Api_SisGestion.Models;
using System.Data.SqlClient;
using System.Data;

namespace Api_SisGestion.Repositories
{
    public class VentaRepository
    {
        public dynamic conn { get; set; }
        public VentaRepository(dynamic conn)
        {
            this.conn = conn;
        }

        public List<Venta> VentaList()
        {
            List<Venta> lista = new();

            using (SqlCommand cmd = new("Select a.*, concat(b.Nombre,' ',b.Apellido) as ApeNomUsuario from  Venta a inner join Usuario b on b.Id = a.IdUsuario ", conn))
            {

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lista.Add(LeerDatos(reader,true));
                    }
                }
            }

            return lista;
        }

        public Venta VentaGet(int id)
        {
            Venta venta = new();

            using (SqlCommand cmd = new("SELECT *, '' as ApeNomUsuario FROM Venta where Id = @id ", conn))
            {


                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    venta = LeerDatos(reader,true);


                }
            }
            return venta;
        }

        public object VentaAdd(Venta data)
        {
            using (SqlCommand cmd = new("Insert into Venta ([Comentarios],[IdUsuario] ) " +
                                                "Values (@comentarios,@idusuario ); select @@IDENTITY; ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = data.Comentarios!.ToUpper() });
                cmd.Parameters.Add(new SqlParameter("idusuario", SqlDbType.Int) { Value = int.Parse(data.IdUsuario.ToString()) });
                
                return cmd.ExecuteScalar();
            }
        }

        public int VentaUpdate(Venta data)
        {
            using (SqlCommand cmd = new("SELECT *, '' as ApeNomUsuario  FROM Venta where Id = @id ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = data.Id });

                Venta venta = new();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    venta = LeerDatos(reader,false);

                    reader.Close();

                    using (SqlCommand cmd2 = new("Update  Venta " +
                                       "SET [Comentarios] = @comentarios, [IdUsuario]=@idusuario " +
                                        "where Id = @id ", conn))
                    {
                        cmd2.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = data.Id });

                        if (venta.Comentarios != data.Comentarios)
                        {
                            cmd2.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = data.Comentarios!.ToUpper() });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios! });
                        }

                        if (venta.IdUsuario != data.IdUsuario)
                        {
                            cmd2.Parameters.Add(new SqlParameter("idusuario", SqlDbType.Int) { Value = data.IdUsuario! });
                        }
                        else
                        {
                            cmd2.Parameters.Add(new SqlParameter("idusuario", SqlDbType.Int) { Value = venta.IdUsuario! });
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

        public int VentaDelete(int Id)
        {
            //primero se busca si hay productos vendidos: para devolver su stock al producto, y eliminarlo de la venta
            using (SqlCommand cmd = new("SELECT * FROM ProductoVendido where IdVenta = @idventa ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("idventa", SqlDbType.Int) { Value = Id });

                SqlDataReader items = cmd.ExecuteReader();
                if (items.HasRows)
                {
                    while (items.Read())
                    {
                        // reintegra stock a productos
                        ProductoVendidoRepository pvrep = new(conn);
                        pvrep.ReintegraStock(int.Parse(items["IdProducto"].ToString()!), int.Parse(items["Stock"].ToString()!));

                        //borra items de la venta
                        pvrep.EliminaItem(int.Parse(items["Id"].ToString()!));
                        
                    }
                }

                // borra la venta
                using (SqlCommand cmd4 = new("delete from Venta where Id = @id ", conn))
                {
                    cmd4.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = Id });
                    return cmd4.ExecuteNonQuery();
                }                    
            }            
        }

        private Venta LeerDatos(SqlDataReader reader,bool detail)
        {
            Venta venta = new(int.Parse(reader["Id"].ToString()!),
                                            reader["Comentarios"].ToString()!.ToUpper()!,
                                            int.Parse(reader["IdUsuario"].ToString()!),
                                            null!,
                                            reader["ApeNomUsuario"].ToString()!.ToUpper()!
                                            );
            if (detail) {
                List<ProductoVendido> lista = new();
                
                using (SqlCommand cmd = new("SELECT a.*, b.Descripciones as ProductoDesc FROM ProductoVendido a inner join Producto b on b.Id = a.IdProducto where IdVenta = @idventa ", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("idventa", SqlDbType.Int) { Value = int.Parse(reader["Id"].ToString()!) });

                    SqlDataReader items = cmd.ExecuteReader();
                    if (items.HasRows)
                    {
                        ProductoVendidoRepository pvrep = new(conn);
                        while (items.Read())
                        {
                            ProductoVendido productoVendido = pvrep.LeerDatos(items);

                            lista.Add(productoVendido);
                        }
                    }
                }
                venta.VentaProducto = lista;
                return venta;
            }
            else
            {
                return venta;
            }

        }
    }
}
