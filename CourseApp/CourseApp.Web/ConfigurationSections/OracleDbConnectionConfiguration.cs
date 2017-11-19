using CourseApp.DataAccess;
using System.Configuration;

namespace CourseApp.Web.ConfigurationSections
{
    public class OracleDbConnectionConfiguration : ConfigurationSection, IOracleDbConnectionSettings
    {
        [ConfigurationProperty("user")]
        public string User
        {
            get { return (string)this["user"]; }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get { return (string)this["password"]; }
        }

        [ConfigurationProperty("host")]
        public string Host
        {
            get { return (string)this["host"]; }
        }

        [ConfigurationProperty("port")]
        public string Port
        {
            get { return (string)this["port"]; }
        }

        [ConfigurationProperty("sid")]
        public string Sid
        {
            get { return (string)this["sid"]; }
        }

        [ConfigurationProperty("serviceName")]
        public string ServiceName
        {
            get { return (string)this["serviceName"]; }
        }

        [ConfigurationProperty("serviceInstance")]
        public string ServiceInstance
        {
            get { return (string)this["serviceInstance"]; }
        }

        [ConfigurationProperty("asSysDba")]
        public bool AsSysDba
        {
            get { return (bool)this["asSysDba"]; }
        }
    }
}
