using System;
using System.Collections.Generic;
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

        ///// <summary>
        ///// Get a DbCommand Object, that matches the Provider
        ///// </summary>
        ///// <returns>DBCommand matching the Provider</returns>
        //public virtual DbCommand GetDbCommand()
        //{
        //    throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        //}


        /// <summary>
        /// Set the command timeout for the connection in seconds
        /// </summary>
        /// <param name="seconds">Timeout in seconds</param>
        public virtual void SetCommandTimeout(int seconds)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }

        /// <summary>
        /// Get status messages from database stored procs
        /// </summary>
        public SqlStatus SendStatus { get; set; }

        /// <summary>
        /// Run SQL statement directly against database
        /// </summary>
        /// <param name="sql">SQL statement</param>
        /// <param name="async">Run async?</param>
        public virtual void Exec(string sql, bool async = false)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }

        /// <summary>
        /// Run a lot of commands on one connection in order of the list
        /// </summary>
        /// <param name="commands">List of commands</param>
        /// <returns>0 if there was no error, command's index if there was an error</returns>
        public virtual int ExecMultiple(IList<DbCommand> commands)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Exec a SQL statement and return a scalar value as string
        /// </summary>
        /// <param name="sql">SQL statement</param>
        /// <returns>Scalar value as string</returns>
        public virtual string ExecWithResult(string sql)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }


        /// <summary>
        /// Exec a SQL command and return a scalar value as string
        /// </summary>
        /// <param name="cmd">SQL command to run</param>
        /// <returns>Scalar value as string</returns>
        public virtual string ExecWithResult(DbCommand cmd)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }


        /// <summary>
        /// Get a data table from an SQL statement
        /// </summary>
        /// <param name="sql">SQL statement to run</param>
        /// <returns>An open <see cref="DbDataReader"/> object</returns>
        public virtual DbDataReader GetDataReader(string sql)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);

        }


        /// <summary>
        /// Get a <see cref="DataRow"/> object from a SQL statement
        /// </summary>
        /// <param name="sql">SQL statement to run</param>
        /// <returns>A <see cref="DataRow"/> object with data</returns>
        public virtual object[] GetDataRow(string sql)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }


        /// <summary>
        /// Get a data table from an SQL statement
        /// </summary>
        /// <param name="sql">SQL statement to run</param>
        /// <returns>A <see cref="DataTable"/> object with data</returns>
        public virtual DataTable GetDataTable(string sql)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }


        /// <summary>
        /// Get a <see cref="DataAdapter"/> from an SQL statement
        /// </summary>
        /// <param name="sql">SQL statement</param>
        /// <returns>A <see cref="DataAdapter"/> object with data</returns>   
        public virtual DataAdapter GetDataAdapter(string sql)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }

        /// <summary>
        /// Get a <see cref="DataAdapter"/> from an <see cref="DbCommand"/>
        /// </summary>
        /// <param name="cmd">SQL statement</param>
        /// <returns>A <see cref="DataAdapter"/> object with data</returns>   
        public virtual DataAdapter GetDataAdapter(DbCommand cmd)
        {
            throw new DbConnException("The method or operation is not implemented. This is just the adapter class (http://www.dofactory.com/Patterns/PatternAdapter.aspx).", DbConnErrorCode.ERC_NOTIMPLEMENTED);
        }


        /// <summary>
        /// Get a data table from an SQL command
        /// </summary>
        /// <param name="cmd">SQL command to run</param>
        /// <returns>A <see cref="DataTable"/> object with data</returns>
        public virtual DataTable GetDataTable(DbCommand cmd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a command object that implements <see cref="DbCommand"/> for the current database
        /// </summary>
        /// <returns></returns>
        public virtual DbCommand GetCommand()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Get a parameter for the provided command
        /// </summary>
        /// <param name="cmd">Current command type</param>
        /// <param name="parameterName">Name of the parameter</param>
        /// <param name="dataType">General database data typeof the parameter</param>
        /// <returns>Parameter object to set value for</returns>
        public virtual DbParameter GetParameter(DbCommand cmd, string parameterName, GeneralDbType dataType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a DataReader for a DbCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>An open <see cref="DbDataReader"/> object</returns>
        public virtual DbDataReader GetDataReader(DbCommand cmd)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Run SQL statement directly against database
        /// </summary>
        /// <param name="cmd">SQL statement to run</param>      
        public virtual void Exec(DbCommand cmd)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Run command async
        /// </summary>
        /// <param name="cmd">Command to run</param>
        public virtual void ExecAsync(DbCommand cmd)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}