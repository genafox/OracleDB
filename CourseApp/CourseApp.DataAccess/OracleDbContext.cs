using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.DataAccess
{
    public class OracleDbContext
    {
        public static string ConnectionString;

        public static void Setup(IOracleDbConnectionSettings connectionSettings)
        {
            string dataSource =
                "(DESCRIPTION = " +
                        $"(ADDRESS = " +
                            $"(PROTOCOL = TCP)" +
                            $"(HOST = {connectionSettings.Host})" +
                            $"(PORT = {connectionSettings.Port}))" +
                        "(CONNECT_DATA = " +
                            $"(SID={connectionSettings.Sid})" +
                            $"(SERVICE_NAME = {connectionSettings.ServiceName})" +
                            $"(INSTANCE_NAME = {connectionSettings.ServiceInstance})))";

            string connectAsSysDba = connectionSettings.AsSysDba
                    ? $"DBA Privilege=SYSDBA;"
                    : string.Empty;

            ConnectionString = 
                $"User Id={connectionSettings.User};" +
                $"Password={connectionSettings.Password};" +
                $"Data Source={dataSource};" +
                connectAsSysDba +
                "Min Pool Size=10;" +
                "Connection Lifetime=120;" +
                "Connection Timeout=60;" +
                "Incr Pool Size=5; " +
                "Decr Pool Size=2";
        }

        public IEnumerable<T> ExecuteQuery<T>(string query, Func<OracleDataReader, T> createEntityFunc)
        {
            var entities = new List<T>();
            using (OracleConnection con = new OracleConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();

                var command = new OracleCommand(query, con);

                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entities.Add(createEntityFunc(reader));
                    }
                }

                con.Close();
            }

            return entities;
        }
    }
}
