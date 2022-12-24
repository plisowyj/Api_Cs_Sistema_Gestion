using Api_SisGestion.Models;
using System.Data.SqlClient;
using System.Data;

namespace Api_SisGestion.Repositories
{
    public class ProductoVendidoRepository
    {
        public dynamic conn { get; set; }
        public ProductoVendidoRepository(dynamic conn)
        {
            this.conn = conn;
        }

        public List<ProductoVendido> ProdVendidoList(int idventa)
        {
            List<ProductoVendido> lista = new();

            using (SqlCommand cmd = new("SELECT a.*, b.Descripciones as ProductoDesc FROM ProductoVendido a inner join Producto b on b.Id = a.IdProducto where IdVenta = @idventa ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("idventa", SqlDbType.Int) { Value = idventa });

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

        public ProductoVendido ProdVendidoGet(int idventa, int id)
        {
            ProductoVendido prodvend = new();

            using (SqlCommand cmd = new("SELECT * FROM ProductoVendido where IdVenta = @idventa and Id = @id ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("idventa", SqlDbType.Int) { Value = idventa });
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    prodvend = LeerDatos(reader);


                }
            }
            return prodvend;
        }

        public object ProdVendidoAdd(ProductoVendido data)
        {
            using (SqlCommand cmd2 = new("update Producto set Stock=Stock-@stock where Id = @idproducto ", conn))
            {
                cmd2.Parameters.Add(new SqlParameter("idproducto", SqlDbType.Int) { Value = int.Parse(data.IdProducto.ToString()) });
                cmd2.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = int.Parse(data.Stock.ToString()) });
                cmd2.ExecuteNonQuery();

                using (SqlCommand cmd = new("Insert into ProductoVendido ([Stock],[IdProducto], [IdVenta] ) " +
                                                "Values (@stock,@idproducto,@idventa ); select @@IDENTITY; ", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = int.Parse(data.Stock!.ToString()) });
                    cmd.Parameters.Add(new SqlParameter("idproducto", SqlDbType.Int) { Value = int.Parse(data.IdProducto.ToString()) });
                    cmd.Parameters.Add(new SqlParameter("idventa", SqlDbType.Int) { Value = int.Parse(data.IdVenta.ToString()) });

                    return cmd.ExecuteScalar();
                }
            }

        }

        
        public int ProdVendidoDelete(int id)
        {
            using (SqlCommand cmd = new("SELECT * FROM ProductoVendido where Id = @id ", conn))
            {
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    // reintegra stock al producto
                    ReintegraStock(int.Parse(reader["IdProducto"].ToString()!), int.Parse(reader["stock"].ToString()!));
                }
                //borra item 
                return EliminaItem(id);
            }           
                
        }

        public ProductoVendido LeerDatos(SqlDataReader reader)
        {
            return new(int.Parse(reader["Id"].ToString()!),
                        int.Parse(reader["Stock"].ToString()!),
                        int.Parse(reader["IdProducto"].ToString()!),
                        int.Parse(reader["IdVenta"].ToString()!),
                        reader["productoDesc"].ToString()!); 
        }

        public int ReintegraStock(int idproducto, int stock)
        {
            using (SqlCommand cmd2 = new("update Producto set Stock=Stock+@stock where Id = @idproducto ", conn))
            {
                cmd2.Parameters.Add(new SqlParameter("idproducto", SqlDbType.Int) { Value = idproducto });
                cmd2.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = stock });
                return cmd2.ExecuteNonQuery();
            }
        }

        public int EliminaItem(int id)
        {
            using (SqlCommand cmd3 = new("delete from ProductoVendido where Id = @id ", conn))
            {
                cmd3.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                return cmd3.ExecuteNonQuery();
            }
        }
    }
}
