using System;
using System.Data;
using System.Data.Common;

namespace Bodoconsult.Core.Database
{

    /// <summary>
    /// Base connection manager class
    /// </summary>
    public class AdapterConnManager : IConnManager
    {
        protected string ConnectionString;


        #region IConnManager Members

        /// <summary>
        /// Tests the connectionstring.
        /// </summary>
        /// <returns>True= connection could be established; False connection could not be established</returns>
        public virtual bool TestConnection()
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }

        /// <summary>
        /// Get a DbCommand Object, that matches the Provider
        /// </summary>
        /// <returns>DBCommand matching the Provider</returns>
        public virtual DbCommand GetDbCommand()
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }

        /// <summary>
        /// Explicitly Sets a ConnectionTimeout
        /// </summary>
        /// <returns>True= connection could be established; False connection could not be established</returns>
        public virtual void SetCommandTimeout(int seconds)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }

        /// <summary>
        /// Get status messages from database stored procs
        /// </summary>
        public SqlStatus SendStatus { get; set; }

        /// <summary>
        /// Executes a query.
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <param name="async">True = Executes the sqlsrcipt asynchronious. False = Executes the SQL script synchronious. Script can only be executed asynchronious with provider System.Data.SqlClient</param>
        public virtual void Exec(string sql, bool async)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }

        /// <summary>
        /// Executes a query and returns the first column of the first row in the result. If the result is empty it will return a null reference.
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <returns></returns>
        public virtual string ExecWithResult(string sql)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED); 
        }

        /// <summary>
        /// Executes a query and returns the first column of the first row in the result. If the result is empty it will return a null reference.
        /// </summary>
        /// <param name="cmd">SQL script that will be executed </param>
        /// <returns></returns>
        public virtual string ExecWithResult(DbCommand cmd)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }

        /// <summary>
        /// Executes a query and returns a DbDataReader with the result.
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <returns></returns>
        public virtual DbDataReader GetDataReader(string sql)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);

        }

        /// <summary>
        /// Executes a query and returns the first row of the result in an object-array. If the result is empty the function will return a null reference.
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <returns></returns>
        public virtual object[] GetDataRow(string sql)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }

        /// <summary>
        /// Executes a query and returns the result in a DataTable.
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <returns></returns>
        public virtual DataTable GetDataTable(string sql)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }

        /// <summary>
        /// Executes a query and returns the DataAdapter for the connection and the Sqlscript
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <returns></returns>
        public virtual DataAdapter GetDataAdapter(string sql)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }

        /// <summary>
        /// Returns a DataTable for a DbCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual DataTable GetDataTable(DbCommand cmd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a DataReader for a DbCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual  DbDataReader GetDataReader(DbCommand cmd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes cmd
        /// </summary>
        /// <param name="cmd"></param>
        public virtual void Exec(DbCommand cmd)
        {
            throw new NotImplementedException();
        }



        public virtual void ExecAsync(DbCommand cmd)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}