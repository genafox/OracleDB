namespace CourseApp.WebCore.Configuration
{
    public class OracleDbConnectionSettings
    {
        public string User { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

        public string Sid { get; set; }

        public string ServiceName { get; set; }

        public string ServiceInstance { get; set; }

        public bool AsSysDba { get; set; }

        public string GetConnectionString()
        {
            string dataSource =
                "(DESCRIPTION = " +
                        $"(ADDRESS = " +
                            $"(PROTOCOL = TCP)" +
                            $"(HOST = {this.Host})" +
                            $"(PORT = {this.Port}))" +
                        "(CONNECT_DATA = " +
                            $"(SID={this.Sid})" +
                            $"(SERVICE_NAME = {this.ServiceName})" +
                            $"(INSTANCE_NAME = {this.ServiceInstance})))";

            string connectAsSysDba = this.AsSysDba
                    ? $"DBA Privilege=SYSDBA;"
                    : string.Empty;

            return
                $"User Id={this.User};" +
                $"Password={this.Password};" +
                $"Data Source={dataSource};" +
                connectAsSysDba +
                "Min Pool Size=10;" +
                "Connection Lifetime=120;" +
                "Connection Timeout=60;" +
                "Incr Pool Size=5; " +
                "Decr Pool Size=2";
        }
    }
}
