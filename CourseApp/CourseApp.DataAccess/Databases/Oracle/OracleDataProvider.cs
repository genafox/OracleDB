namespace CourseApp.DataAccess.Databases.Oracle
{
    public class OracleDataProvider
    {
        private readonly OracleConnectionSettings connectionSettings;

        public OracleDataProvider(OracleConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings;
        }


    }
}
