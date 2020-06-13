using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Bodoconsult.Core.Database
{
    public class SqlClientConnManager : AdapterConnManager
    {
        private SqlConnection _connection;


        public static AdapterConnManager GetConnManager(string connectionString)
        {
            return new SqlClientConnManager(connectionString);
        }

        public SqlClientConnManager(string connectionString)
        {
            
            ConnectionString = connectionString;

            if (!ConnectionString.ToLower().Contains("connect timeout")) ConnectionString += "; Connect timeout=3000";
        }

        public override bool TestConnection()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    if (SendStatus!=null) conn.InfoMessage += ConnOnInfoMessage;
                    conn.Open();
                    conn.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void ConnOnInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            SendStatus(e.Message);
        }

        /// <summary>
        /// Get a DbCommand Object, that matches the Provider
        /// </summary>
        /// <returns>DBCommand matching the Provider</returns>
        public override DbCommand GetDbCommand()
        {
            var cmd = new SqlCommand();
            return cmd;
        }

        private int _commandTimeOut = -1;

        /// <summary>
        /// Explicitly Sets a ConnectionTimeout
        /// </summary>
        /// <returns>True= connection could be established; False connection could not be established</returns>
        public override void SetCommandTimeout(int seconds)
        {
            _commandTimeOut = seconds;
        }

        /// <summary>
        /// Executes a query and returns the DataAdapter for the connection and the Sqlscript
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <returns></returns>
        public override DataAdapter GetDataAdapter(string sql)
        {
            var conn = new SqlConnection(ConnectionString);
            if (SendStatus != null) conn.InfoMessage += ConnOnInfoMessage;
            return new SqlDataAdapter(sql, conn);
        }

        /// <summary>
        /// Executes a query and returns a DbDataReader with the result.
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <returns></returns>
        public override DbDataReader GetDataReader(string sql)
        {
            try
            {
                var conn = new SqlConnection(ConnectionString);
                if (SendStatus != null) conn.InfoMessage += ConnOnInfoMessage;
                conn.Open();
                var cmd = new SqlCommand(sql, conn);
                if (_commandTimeOut != -1)
                    cmd.CommandTimeout = _commandTimeOut;
                var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("GetDataReader:{0}:Sql:{1}", ConnectionString, sql), ex);
            }
        }

        /// <summary>
        /// Executes a query and returns the result in a DataTable.
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <returns></returns>
        public override DataTable GetDataTable(string sql)
        {
            try
            {
                var conn = new SqlConnection(ConnectionString);
                if (SendStatus != null) conn.InfoMessage += ConnOnInfoMessage;
                conn.Open();
                var cmd = new SqlCommand(sql, conn);
                if (_commandTimeOut != -1)
                    cmd.CommandTimeout = _commandTimeOut;
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                var dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("GetDataTable:{0}:Sql:{1}", ConnectionString, sql), ex);
            }
        }

        /// <summary>
        /// Executes a query and returns the first row of the result in an object-array. If the result is empty the function will return a null reference.
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <returns></returns>
        public override object[] GetDataRow(string sql)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    if (SendStatus != null) conn.InfoMessage += ConnOnInfoMessage;
                    var cmd = new SqlCommand(sql, conn);
                    if (_commandTimeOut != -1)
                        cmd.CommandTimeout = _commandTimeOut;
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        var values = new object[reader.FieldCount];
                        // ReSharper disable ReturnValueOfPureMethodIsNotUsed
                        reader.GetValues(values);
                        // ReSharper restore ReturnValueOfPureMethodIsNotUsed
                        return values;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("GetDataRow:{0}:Sql:{1}", ConnectionString, sql), ex);
            }
        }

        /// <summary>
        /// Executes a query and returns the first column of the first row in the result. If the result is empty it will return a null reference.
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <returns></returns>
        public override string ExecWithResult(string sql)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    if (SendStatus != null) conn.InfoMessage += ConnOnInfoMessage;
                    var cmd = new SqlCommand(sql, conn);
                    if (_commandTimeOut != -1)
                        cmd.CommandTimeout = _commandTimeOut;
                    cmd.Connection.Open();
                    var value = cmd.ExecuteScalar();
                    conn.Close();
                    var val = (value != null) ? value.ToString() : "";
                    return val;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ExecWithResult:{0}:Sql:{1}", ConnectionString, sql), ex);
            }


        }


        /// <summary>
        /// Executes a query and returns the first column of the first row in the result. If the result is empty it will return a null reference.
        /// </summary>
        /// <param name="cmd">SQL script that will be executed </param>
        /// <returns></returns>
        public override string ExecWithResult(DbCommand cmd)
        {

            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    if (SendStatus != null) conn.InfoMessage += ConnOnInfoMessage;
                    cmd.Connection = conn;
                    if (_commandTimeOut != -1)
                        cmd.CommandTimeout = _commandTimeOut;
                    cmd.Connection.Open();
                    var value = cmd.ExecuteScalar();
                    conn.Close();
                    var val = (value != null) ? value.ToString() : "";
                    return val;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("ExecWithResult:{0}:Sql:{1}", ConnectionString, cmd.CommandText), ex);
            }
        }


        /// <summary>
        /// Returns a DataTable for a DbCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public override DataTable GetDataTable(DbCommand cmd)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    if (SendStatus != null) conn.InfoMessage += ConnOnInfoMessage;
                    cmd.Connection = conn;
                    if (_commandTimeOut != -1)
                        cmd.CommandTimeout = _commandTimeOut;
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    var dt = new DataTable();
                    dt.Load(reader);
                    return dt;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("GetDataTable:{0}:Sql:{1}", ConnectionString, cmd.CommandText), ex);
            }


        }


        /// <summary>
        /// Returns a DataReader for a DbCommand
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public override DbDataReader GetDataReader(DbCommand cmd)
        {
            try
            {
                var conn = new SqlConnection(ConnectionString);
                if (SendStatus != null) conn.InfoMessage += ConnOnInfoMessage;
                cmd.Connection = conn;
                if (_commandTimeOut != -1)
                    cmd.CommandTimeout = _commandTimeOut;
                cmd.Connection.Open();
                var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("GetDataReader:{0}:Sql:{1}", ConnectionString, cmd.CommandText), ex);
            }


        }

        /// <summary>
        /// Executes cmd
        /// </summary>
        /// <param name="cmd">SQL statement to run</param>
        public override void Exec(DbCommand cmd)
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    if (SendStatus != null) conn.InfoMessage += ConnOnInfoMessage;
                    cmd.Connection = conn;
                    if (_commandTimeOut != -1)
                        cmd.CommandTimeout = _commandTimeOut;
                    cmd.Connection.Open();
                    //cmd.ExecuteNonQuery();
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Exec:{0}:Sql:{1}", ConnectionString, cmd.CommandText), ex);
            }

        }


        public override void ExecAsync(DbCommand cmd)
        {
            try
            {
                var cs = ConnectionString + ";Asynchronous Processing=true;";
                using (_connection = new SqlConnection(cs))
                {
                    if (SendStatus != null) _connection.InfoMessage += ConnOnInfoMessage;
                    cmd.Connection = _connection;
                    if (_commandTimeOut != -1)
                        cmd.CommandTimeout = _commandTimeOut;
                    cmd.Connection.Open();

                    //var callback = new AsyncCallback(HandleCallback);
                    //((SqlCommand) cmd).BeginExecuteNonQuery(callback, cmd);


                    var sqlcmd = ((SqlCommand)cmd);

                    sqlcmd.ExecuteScalar();

                    //var result = sqlcmd.BeginExecuteNonQuery();
                    //while (!result.IsCompleted)
                    //{
                    //}

                    //sqlcmd.EndExecuteNonQuery(result);


                    //((SqlCommand)cmd).BeginExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ExecAsync:{0}:Sql:{1}", ConnectionString, cmd.CommandText), ex);
            }
        }


        /// <summary>
        /// Executes a query.
        /// </summary>
        /// <param name="sql">SQL script that will be executed</param>
        /// <param name="async">True = Executes the sqlsrcipt asynchronious. False = Executes the SQL script synchronious. Script can only be executed asynchronious with provider System.Data.SqlClient</param>
        public override void Exec(string sql, bool async)
        {
            try
            {
                if (async)
                {
                    var cs = ConnectionString + ";Asynchronous Processing=true;";
                    using (_connection = new SqlConnection(cs))
                    {
                        if (SendStatus != null) _connection.InfoMessage += ConnOnInfoMessage;
                        var cmd = new SqlCommand(sql, _connection);
                        if (_commandTimeOut != -1)
                            cmd.CommandTimeout = _commandTimeOut;
                        cmd.Connection.Open();

                        var callback = new AsyncCallback(HandleCallback);
                        cmd.BeginExecuteNonQuery(callback, cmd);

                    }

                }
                else
                {
                    using (var conn = new SqlConnection(ConnectionString))
                    {
                        if (SendStatus != null) conn.InfoMessage += ConnOnInfoMessage;
                        var cmd = new SqlCommand(sql, conn);
                        if (_commandTimeOut != -1)
                            cmd.CommandTimeout = _commandTimeOut;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Exec:{0}:Sql:{1}", ConnectionString, sql), ex);
            }


        }

        private void HandleCallback(IAsyncResult result)
        {
            try
            {
                var command = (SqlCommand)result.AsyncState;
                command.EndExecuteNonQuery(result);
                if (_connection == null) return;
                try
                {
                    _connection.Close();
                }
                // ReSharper disable EmptyGeneralCatchClause
                catch
                // ReSharper restore EmptyGeneralCatchClause
                {
                    //nop
                }
            }
            catch (Exception ex)
            {

                throw new Exception("HandleCallback", ex);
            }


        }
    }
}