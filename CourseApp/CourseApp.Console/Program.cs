using Oracle.ManagedDataAccess.Client;

namespace CourseApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (OracleConnection con = new OracleConnection())
            {
                //Open a connection using ConnectionString attributes
                //related to connection pooling.
                string userId = "SYS";
                string password = "Password12";
                string hostName = "52.164.202.80";
                string dataSource = "(DESCRIPTION = " +
                    $"(ADDRESS = (PROTOCOL = TCP)(HOST = {hostName})(PORT = 1521))" +
                    "(CONNECT_DATA = (SID=course_app_db)(SERVICE_NAME = course_app_db)(INSTANCE_NAME = course_app_db))" +
                    ")";

                con.ConnectionString = $"User Id={userId};Password={password};Data Source={dataSource};DBA Privilege=SYSDBA;" +
                  "Min Pool Size=10;Connection Lifetime=120;Connection Timeout=60;" +
                  "Incr Pool Size=5; Decr Pool Size=2";
                con.Open();

                var query = new OracleCommand("SELECT * FROM Course_GF", con);

                using (OracleDataReader reader = query.ExecuteReader())
                {
                    reader.Read();
                    int courseId = reader.GetInt32(0);
                }

                con.Close();
            }
        }
    }
}
