
using System.Data.SqlClient;

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

        public static string Organization()
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            IConfiguration _configuration = builder.Build();
            return _configuration.GetConnectionString("Organization");
        }


    }
}
