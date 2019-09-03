using System.Data;
using System.Data.SqlClient;

namespace Conference.Common.Dapper
{
    /// <summary>
    /// Dapper单列连接
    /// </summary>
    public class DapperConnection
    {
        protected static IDbConnection Connection;
        protected static readonly object Locker = new object();
        public static IDbConnection DapperInstance()
        {
            lock (Locker)
            {
                if (Connection == null)
                {
                    var conn = JsonConfigurationHelper.GetAppSettings<ConnectionService>("appsettings.json", "ConnectionService");
                    Connection = new SqlConnection(conn.ConnectionSqlService);
                }
            }
            return Connection;
        }
    }
}
