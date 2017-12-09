using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
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

            Func<OracleCommand, Task<IEnumerable<T>>> func = async (command) =>
            {
                var entities = new List<T>();

                using (DbDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        entities.Add(createEntityFunc(reader));
                    }
                }

                return entities;
            };

            return await this.Execute<IEnumerable<T>>(query, func);
        }

        public async Task ExecuteNonQueryAsync(string commandText)
        {
            Func<OracleCommand, Task<bool>> func = async (command) =>
            {
                await command.ExecuteNonQueryAsync();

                return true;
            };

            await this.Execute<bool>(commandText, func);
        }

        public async Task ExecuteProcedureAsync(string procedureName, params OracleParameter[] parameters)
        {
            Func<OracleCommand, Task<bool>> func = async (command) =>
            {
                command.Parameters.AddRange(parameters);

                await command.ExecuteNonQueryAsync();

                return true;
            };

            await this.Execute<bool>(procedureName, func);
        }

        public async Task<T> ExecuteScalarAsync<T>(string commandText)
        {
            Func<OracleCommand, Task<T>> func = async (command) =>
            {
                object result = await command.ExecuteScalarAsync();

                return (T)result;
            };

            return await this.Execute<T>(commandText, func);
        }

        private async Task<T> Execute<T>(string commandText, Func<OracleCommand, Task<T>> executeCommandAsyncFunc)
        {
            using (var connection = new OracleConnection(this.connectionString))
            {
                using (var command = new OracleCommand(commandText, connection))
                {
                    connection.Open();

                    T result = await executeCommandAsyncFunc(command);

                    return result;
                }
            }

        }
    }
}
