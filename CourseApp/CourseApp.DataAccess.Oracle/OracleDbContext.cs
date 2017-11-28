using CourseApp.WebCore.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace CourseApp.DataAccess.Oracle
{
    public class OracleDbContext : IDisposable
    {
        private bool disposedValue = false;

        private readonly OracleConnection connection;

        public OracleDbContext(OracleDbConnectionSettings connectionSettings)
        {
            string connectionString = connectionSettings.GetConnectionString();

            this.connection = new OracleConnection(connectionString);
            this.connection.Open();
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, Func<DbDataReader, T> createEntityFunc)
        {
            var entities = new List<T>();
            var command = new OracleCommand(query, this.connection);

            using (DbDataReader reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    entities.Add(createEntityFunc(reader));
                }
            }

            return entities;
        }

        public async Task<T> ExecuteCommandAsync<T>(string commandText)
        {
            var command = new OracleCommand(commandText, this.connection);

            object result = await command.ExecuteScalarAsync();

            return (T)result;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.connection.Close();
                    this.connection.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}
