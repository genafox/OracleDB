using System;

namespace CourseApp.DataAccess.Databases.Oracle
{
    public class ConnectionSettings
    {
        public ConnectionSettings(Uri agentUri)
        {
            this.AgentUri = agentUri;
        }

        public Uri AgentUri { get; set; }
    }
}
