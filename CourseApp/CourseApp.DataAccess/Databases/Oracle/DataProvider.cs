using System;

namespace CourseApp.DataAccess.Databases.Oracle
{
    public class DataProvider
    {
        private readonly Uri oracleAgentUri;

        public DataProvider(Uri oracleAgentUri)
        {
            this.oracleAgentUri = oracleAgentUri;
        }
    }
}
