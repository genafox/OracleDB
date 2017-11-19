namespace CourseApp.DataAccess
{
    public interface IOracleDbConnectionSettings
    {
        string User { get; }

        string Password { get; }

        string Host { get; }

        string Port { get; }

        string Sid { get; }

        string ServiceName { get; }

        string ServiceInstance { get; }

        bool AsSysDba { get; }
    }
}
