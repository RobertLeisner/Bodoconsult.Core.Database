using System.Collections.Generic;
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
        void Exec(string sql, bool async=false);

        /// <summary>
        /// Run SQL statement directly against database
        /// </summary>
        /// <param name="cmd">SQL statement to run</param>      
        void Exec(DbCommand cmd);

        /// <summary>
        /// Run a lot of commands on one connection in order of the list
        /// </summary>
        /// <param name="commands">List of commands</param>
        /// <returns>0 if there was no error, command's index if there was an error</returns>
        int ExecMultiple(IList<DbCommand> commands);

        /// <summary>
        /// Exec a SQL statement and return a scalar value as string
        /// </summary>
        /// <param name="sql">SQL statement</param>
        /// <returns>Scalar value as string</returns>
        string ExecWithResult(string sql);

        /// <summary>
        /// Exec a SQL command and return a scalar value as string
        /// </summary>
        /// <param name="cmd">SQL command to run</param>
        /// <returns>Scalar value as string</returns>
        string ExecWithResult(DbCommand cmd);

        /// <summary>
        /// Get a data table from an SQL command
        /// </summary>
        /// <param name="cmd">SQL command to run</param>
        /// <returns>A <see cref="DataTable"/> object with data</returns>
        DataTable GetDataTable(DbCommand cmd);


        /// <summary>
        /// Get a data table from an SQL command
        /// </summary>
        /// <param name="cmd">SQL command to run</param>
        /// <returns>Open <see cref="DataTable"/> object</returns>
        DbDataReader GetDataReader(DbCommand cmd);

        /// <summary>
        /// Get a data table from an SQL statement
        /// </summary>
        /// <param name="sql">SQL statement to run</param>
        /// <returns>Open <see cref="DataTable"/> object</returns>
        DbDataReader GetDataReader(string sql);

        /// <summary>
        /// Get a <see cref="DataRow"/> object from a SQL statement
        /// </summary>
        /// <param name="sql">SQL statement to run</param>
        /// <returns>A <see cref="DataRow"/> object with data</returns>
        object[] GetDataRow(string sql);

        /// <summary>
        /// Get a data table from an SQL statement
        /// </summary>
        /// <param name="sql">SQL statement to run</param>
        /// <returns>A <see cref="DataTable"/> object with data</returns>
        DataTable GetDataTable(string sql);


        /// <summary>
        /// Get a <see cref="DataAdapter"/> from an SQL statement
        /// </summary>
        /// <param name="sql">SQL statement</param>
        /// <returns>A <see cref="DataAdapter"/> object with data</returns>   
        DataAdapter GetDataAdapter(string sql);

        /// <summary>
        /// Test the connection
        /// </summary>
        /// <returns>true on success else false</returns>
        bool TestConnection();
        
        /// <summary>
        /// Set the command timeout for the connection in seconds
        /// </summary>
        /// <param name="seconds">Timeout in seconds</param>
        void SetCommandTimeout(int seconds);

        /// <summary>
        /// Run command async
        /// </summary>
        /// <param name="cmd">Command to run</param>
        void ExecAsync(DbCommand cmd);

        ///// <summary>
        ///// Get a SQL command object based on the current connection
        ///// </summary>
        ///// <returns></returns>
        //DbCommand GetDbCommand();
    }
}