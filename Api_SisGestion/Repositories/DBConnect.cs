
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api_SisGestion.Repositories
{
    public class DBConnect
    {
        public static dynamic ConnDB()
        {
            if (true)
            {
                var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
                IConfiguration _configuration = builder.Build();
                var strConn = _configuration.GetConnectionString("myDb");
                                
                try
                {
                    SqlConnection? conn = new SqlConnection(strConn);
                    conn.Open();

                    return conn;
                }
                catch (Exception ex)
                {                    
                    return ex.Message;
                }
            }
            else {
            
            }


        }


    }
}
