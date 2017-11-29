using CourseApp.DataAccess.Interfaces;
using System.Configuration;

namespace CourseApp.DataAccess.Oracle
{
    public class OracleDbConnectionSettings : ConfigurationSection
    {
        [ConfigurationProperty("user", IsRequired = true)]
        public string User
        {
            get { return this["user"] as string; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return this["password"] as string; }
        }

        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get { return this["host"] as string; }
        }

        [ConfigurationProperty("port", IsRequired = true)]
        public string Port
        {
            get { return this["port"] as string; }
        }

        [ConfigurationProperty("sid", IsRequired = true)]
        public string Sid
        {
            get { return this["sid"] as string; }
        }

        [ConfigurationProperty("serviceName", IsRequired = true)]
        public string ServiceName
        {
            get { return this["serviceName"] as string; }
        }

        [ConfigurationProperty("serviceInstance", IsRequired = true)]
        public string ServiceInstance
        {
            get { return this["serviceInstance"] as string; }
        }

        [ConfigurationProperty("asSysDba", IsRequired = true)]
        public bool AsSysDba
        {
            get { return (bool)this["asSysDba"]; }
        }

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
