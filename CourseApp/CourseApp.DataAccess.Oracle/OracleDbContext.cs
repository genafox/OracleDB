using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace CourseApp.DataAccess.Oracle
{
    public class OracleDbContext
    {
        private readonly string connectionString;

        public OracleDbContext(OracleDbConnectionSettings connectionSettings)
        {
            this.connectionString = connectionSettings.GetConnectionString();
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, Func<DbDataReader, T> createEntityFunc)
        {
            var entities = new List<T>();

            using (var connection = new OracleConnection(this.connectionString))
            {
                using (var command = new OracleCommand(query, connection))
                {
                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            entities.Add(createEntityFunc(reader));
                        }
                    }

                    return entities;
                }
            }
        }

        public async Task ExecuteNonQueryAsync(string commandText)
        {
            using (var connection = new OracleConnection(this.connectionString))
            {
                using (var command = new OracleCommand(commandText, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task ExecuteProcedureAsync(string procedureName, params OracleParameter[] parameters)
        {
            using (var connection = new OracleConnection(this.connectionString))
            {
                using (var command = new OracleCommand(procedureName, connection))
                {
                    command.Parameters.AddRange(parameters);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string commandText)
        {
            using (var connection = new OracleConnection(this.connectionString))
            {
                using (var command = new OracleCommand(commandText, connection))
                {
                    object result = await command.ExecuteScalarAsync();

                    return (T)result;
                }
            }
        }
    }
}
