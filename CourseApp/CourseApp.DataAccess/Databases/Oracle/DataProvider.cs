namespace CourseApp.DataAccess.Databases.Oracle
{
    public class DataProvider
    {
        private readonly ConnectionSettings connectionSettings;

        public DataProvider(ConnectionSettings connectionSettings)
        {
            this.connectionSettings = connectionSettings;
        }


    }
}
