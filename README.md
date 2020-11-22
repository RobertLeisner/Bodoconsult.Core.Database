# What does the library

Attention: Migrated all SqlServer related stuff like SqlClientConnManager with package 1.05 to Bodoconsult.Core.Database (Date: 11/22/2020)!!!!

Bodoconsult.Core.Database library simplifies the access to SQL based databases. This library is as base class which does not contain implementations. 
It handles the most common database actions like getting datatables, running SQL statement or fetch scalar values from the database.

See the following implementations:

Bodoconsult.Core.Database.SqlClient for Microsoft SqlServer, Microsoft SqlServer Express or LocalDb based databases (Nuget: <https://www.nuget.org/packages/Bodoconsult.Core.Database.SqlClient/>)

Bodoconsult.Core.Database.Postgres for PostgreSQL based databases (Nuget: <https://www.nuget.org/packages/Bodoconsult.Core.Database.Postgres/>)

# How to use the library

This sample is taken from the implementation Bodoconsult.Core.Database.SqlClient.

The source code contain a NUnit test classes, the following source code is extracted from. The samples below show the most helpful use cases for the library.


    [TestFixture]
    public class TestsSqlClientConnManager
    {

        private readonly string _connectionString = TestHelper.SqlServerString;


        private IConnManager _db;

        [SetUp]
        public void Setup()
        {
            _db = SqlClientConnManager.GetConnManager(_connectionString);

        }

        /// <summary>
        /// Test the connection
        /// </summary>
        [Test]
        public void TestTestConnection()
        {

            var erg = _db.TestConnection();


            Assert.IsTrue(erg);
        }


        /// <summary>
        /// Get a datatable from the database from a plain SQL string (avoid this option due to SQL injection)
        /// </summary>
        [Test]
        public void TestGetDataTableFromSql()
        {

            const string sql = "SELECT * FROM dbo.Customer";

            var erg = _db.GetDataTable(sql);

            Assert.IsTrue(erg.Rows.Count>0);
        }



        /// <summary>
        /// Get a datatable from the database from a (parameterized) SqlCommand object (choose this option to avoid SQL injection)
        /// </summary>
        [Test]
        public void TestGetDataTableFromCommand()
        {

            const string sql = "SELECT * FROM dbo.Customer";


            var cmd = new SqlCommand
            {
                CommandText = sql
            };

            // Add parameters here if required

            var erg = _db.GetDataTable(cmd);


            Assert.IsTrue(erg.Rows.Count > 0);
        }


        /// <summary>
        /// Get a datatable from the database from a (parameterized) SqlCommand object (choose this option to avoid SQL injection)
        /// </summary>
        [Test]
        public void TestGetDataTableFromCommandWidthGetCommand()
        {
            const string sql = "SELECT * FROM dbo.Customer";

            var cmd = _db.GetCommand();
            cmd.CommandText = sql;

            // Add parameters here if required
            var erg = _db.GetDataTable(cmd);


            Assert.IsTrue(erg.Rows.Count > 0);
        }


        /// <summary>
        /// Get a datatable from the database from a (parameterized) SqlCommand object (choose this option to avoid SQL injection)
        /// </summary>
        [Test]
        public void TestGetDataTableFromCommandWidthGetCommandWithParameter()
        {
            const string sql = "SELECT * FROM dbo.Customer WHERE CustomerId=@ID";

            var cmd = _db.GetCommand();
            cmd.CommandText = sql;

            var p = _db.GetParameter(cmd, "@ID", GeneralDbType.Int);
            p.Value = 1;

            // Add parameters here if required
            var erg = _db.GetDataTable(cmd);


            Assert.IsTrue(erg.Rows.Count > 0);
            Assert.IsTrue(erg.Rows.Count == 1);
        }


        /// <summary>
        /// Get a datatable from the database from a plain SQL string (avoid this option due to SQL injection)
        /// </summary>
        [Test]
        public void TestGetDataReaderFromSql()
        {

            const string sql = "SELECT * FROM dbo.Customer";

            var erg = _db.GetDataReader(sql);

            Assert.IsTrue(erg.FieldCount > 0);
        }



        /// <summary>
        /// Get a datatable from the database from a (parameterized) SqlCommand object (choose this option to avoid SQL injection)
        /// </summary>
        [Test]
        public void TestGetDataReaderFromCommand()
        {

            const string sql = "SELECT * FROM dbo.Customer";


            var cmd = new SqlCommand
            {
                CommandText = sql
            };

            // Add parameters here if required

            var erg = _db.GetDataReader(cmd);

            Assert.IsTrue(erg.FieldCount > 0);
        }


        /// <summary>
        /// Get a datatable from the database from a (parameterized) SqlCommand object (choose this option to avoid SQL injection)
        /// </summary>
        [Test]
        public void TestGetDataReaderFromCommandWidthGetCommand()
        {
            const string sql = "SELECT * FROM dbo.Customer";

            var cmd = _db.GetCommand();
            cmd.CommandText = sql;

            // Add parameters here if required
            var erg = _db.GetDataReader(cmd);

            Assert.IsTrue(erg.FieldCount>0);

        }


        /// <summary>
        /// Get a datatable from the database from a (parameterized) SqlCommand object (choose this option to avoid SQL injection)
        /// </summary>
        [Test]
        public void TestGetDataReaderFromCommandWidthGetCommandAndParameter()
        {
            const string sql = "SELECT * FROM dbo.Customer WHERE [CustomerId]=@ID;";

            var cmd = _db.GetCommand();
            cmd.CommandText = sql;

            var p = _db.GetParameter(cmd, "@ID", GeneralDbType.Int);
            p.Value = 1;

            // Add parameters here if required
            var erg = _db.GetDataReader(cmd);

            Assert.IsTrue(erg.FieldCount > 0);
            Assert.IsTrue(erg.HasRows);

        }

        /// <summary>
        /// Execute a plain SQL string (avoid this option due to SQL injection)
        /// </summary>
        [Test]
        public void TestExecFromSql()
        {

            const string sql = "DELETE FROM dbo.Customer WHERE CustomerId=-99";

            Assert.DoesNotThrow(() => _db.Exec(sql));
        }


        /// <summary>
        /// Execute a SqlCommand object (choose this option to avoid SQL injection)
        /// </summary>
        [Test]
        public void TestExecFromCommand()
        {

            const string sql = "DELETE FROM dbo.Customer WHERE CustomerId=@Key";

            // Create command
            var cmd = new SqlCommand
            {
                CommandText = sql
            };

            // Add a parameter to the command
            var para = cmd.Parameters.Add("@Key", SqlDbType.Int);
            para.Value = -99;

            Assert.DoesNotThrow(() => _db.Exec(cmd));
        }


        /// <summary>
        /// Get a scalar value from database from a plain SQL string (avoid this option due to SQL injection)
        /// </summary>
        [Test]
        public void TestExecWithResultFromSql()
        {

            const string sql = "SELECT CustomerId FROM dbo.Customer WHERE CustomerId=1";

            var result = _db.ExecWithResult(sql);

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }


        /// <summary>
        /// Get a scalar value from database from SqlCommand object (choose this option to avoid SQL injection)
        /// </summary>
        [Test]
        public void TestExecWithResultFromCommand()
        {

            const string sql = "SELECT CustomerId FROM dbo.Customer WHERE CustomerId=@Key";

            // Create command
            var cmd = new SqlCommand
            {
                CommandText = sql
            };

            // Add a parameter to the command
            var para = cmd.Parameters.Add("@Key", SqlDbType.Int);
            para.Value = 1;

            var result = _db.ExecWithResult(cmd);

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }


        [Test]
        public void TestExecMultiple()
        {
            const string sql = "DELETE FROM Customer WHERE CustomerId=-99";

            var commands = new List<DbCommand>();

            var cmd = new SqlCommand(sql);
            commands.Add(cmd);

            cmd = new SqlCommand(sql);
            commands.Add(cmd);

            cmd = new SqlCommand(sql);
            commands.Add(cmd);

            var result = _db.ExecMultiple(commands);

            Assert.IsTrue(result == 0);
        }
    }


# About us

Bodoconsult (<http://www.bodoconsult.de>) is a Munich based software development company from Germany.

Robert Leisner is senior software developer at Bodoconsult. See his profile on <http://www.bodoconsult.de/Curriculum_vitae_Robert_Leisner.pdf>.

