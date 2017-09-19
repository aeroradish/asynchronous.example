using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronous.Example.Console
{
    public class DALAsync
    {
        public static string DefaultConnectionString =
                    System.Configuration.ConfigurationManager.ConnectionStrings["default"]
                        .ConnectionString;
        public string ConnectionString;
        protected SqlConnection _connection;
        protected SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = GetConnection();
                return _connection;
            }
            set { _connection = value; }
        }

        public static SqlConnection GetDefaultConnection()
        {
            return new SqlConnection(DefaultConnectionString);
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString ?? DefaultConnectionString);
        }

        public DALAsync()
        {
            ConnectionString = DefaultConnectionString;
        }

        public DALAsync(string connectionString)            
        {
            ConnectionString = connectionString;
        }

        public async Task<int> InformationInsertAsync(Information model)
        {

            return await WithConnection(async c =>
            {
                var p = new DynamicParameters();
                p.Add("Description", model.Description, DbType.String);
                p.Add("Col1", model.Col1, DbType.String);
                p.Add("Col2", model.Col2, DbType.String);
                p.Add("Col3", model.Col3, DbType.String);
                p.Add("Col4", model.Col4, DbType.String);

                p.Add("Col5", model.Col5, DbType.String);
                p.Add("Col6", model.Col6, DbType.String);
                p.Add("Col7", model.Col7, DbType.String);
                p.Add("Col8", model.Col8, DbType.String);

                var dda = await c.ExecuteAsync(
                    sql: "dbo.InformationInsert"
                    , param: p
                    , commandType: CommandType.StoredProcedure

                    );
                return dda;
            });

        }


        public async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    await connection.OpenAsync();
                    return await getData(connection);

                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection experienced a SQL Timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection experienced a SQL exception", GetType().FullName), ex);
            }
        }

    }
}
