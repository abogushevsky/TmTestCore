using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Dapper;
using TaskManager.DataLayer.Common.Exceptions;

namespace TaskManager.DataLayer.MsSql
{
    public abstract class SqlRepositoryBase
    {
        private readonly string connectionString;

        protected SqlRepositoryBase(string connectionStringName)
        {
            Contract.Requires(!string.IsNullOrEmpty(connectionStringName));

            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            this.connectionString = connectionStringSettings.ConnectionString;
        }

        protected async Task<IEnumerable<TResult>> UsingConnectionAsync<TResult>(SqlCommandInfo command, object param)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    await connection.OpenAsync();
                    
                    transaction = connection.BeginTransaction();
                    IEnumerable<TResult> result = await connection.QueryAsync<TResult>(command.Command, param: param,
                        commandType: command.CommandType, transaction: transaction);

                    transaction.Commit();
                    return result;
                }
                catch (SqlException sqlEx)
                {
                    if (transaction != null) transaction.Rollback();
                    if (sqlEx.ErrorCode == ConcurrentUpdateException.ERROR_CODE)
                    {
                        throw new ConcurrentUpdateException();
                    }

                    throw new RepositoryException();
                }
                catch (Exception ex)
                {
                    if (transaction != null) transaction.Rollback();
                    throw new RepositoryException();
                }
            }
        }
    }
}