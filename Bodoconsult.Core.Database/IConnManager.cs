using System.Data;
using System.Data.Common;

namespace Bodoconsult.Core.Database
{
    /// <summary>
    /// Get status messages from database stored procs
    /// </summary>
    /// <param name="message">Message received from database</param>
    public delegate void SqlStatus(string message);

    /// <summary>
    /// Represents a database connection and important tasks for it
    /// </summary>
    public interface IConnManager
    {
        /// <summary>
        /// Get status messages from database stored procs
        /// </summary>
        SqlStatus SendStatus { get; set; }



        /// <summary>
        /// Run SQL statement directly against database
        /// </summary>
        /// <param name="sql">SQL statement</param>
        /// <param name="async">Run async?</param>
        void Exec(string sql, bool async);

        /// <summary>
        /// Run SQL statement directly against database
        /// </summary>
        /// <param name="cmd">SQL statement to run</param>      
        void Exec(DbCommand cmd);

        string ExecWithResult(string sql);
        string ExecWithResult(DbCommand cmd);
        DataTable GetDataTable(DbCommand cmd);
        DbDataReader GetDataReader(DbCommand cmd);
        DbDataReader GetDataReader(string sql);
        object[] GetDataRow(string sql);
        DataTable GetDataTable(string sql);
        DataAdapter GetDataAdapter(string sql);
        bool TestConnection();
        void SetCommandTimeout(int seconds);
        DbCommand GetDbCommand();
    }
}