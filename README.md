# What does the library

Bodoconsult.Core.Database library simplifies the access to Microsoft SqlServer based databases. 
It handles the most common database actions like getting datatables, running SQL statement or fetch scalar values from the database.


# How to use the library

The source code contain a NUnit test classes, the following source code is extracted from. The samples below show the most helpful use cases for the library.

    [TestFixture]
    public class TestsSqlClientConnManager
    {

        private const string ConnectionString =
            @"SERVER=.\sqlexpress;DATABASE=MediaDb;Trusted_Connection=True;MultipleActiveResultSets=true";


        private AdapterConnManager _db;

        [SetUp]
        public void Setup()
        {
            _db = SqlClientConnManager.GetConnManager(ConnectionString);

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

            const string sql = "SELECT * FROM dbo.settings";

            var erg = _db.GetDataTable(sql);


            Assert.IsTrue(erg.Rows.Count>0);
        }



        /// <summary>
        /// Get a datatable from the database from a (parameterized) SqlCommand object (choose this option to avoid SQL injection)
        /// </summary>
        [Test]
        public void TestGetDataTableFromCommand()
        {

            const string sql = "SELECT * FROM dbo.settings";


            var cmd = new SqlCommand
            {
                CommandText = sql
            };

            // Add parameters here if required

            var erg = _db.GetDataTable(cmd);


            Assert.IsTrue(erg.Rows.Count > 0);
        }

        /// <summary>
        /// Execute a plain SQL string (avoid this option due to SQL injection)
        /// </summary>
        [Test]
        public void TestExecFromSql()
        {

            const string sql = "DELETE FROM dbo.settings WHERE skey='XXX'";

            Assert.DoesNotThrow(() => _db.Exec(sql, false));
        }


        /// <summary>
        /// Execute a SqlCommand object (choose this option to avoid SQL injection)
        /// </summary>
        [Test]
        public void TestExecFromCommand()
        {

            const string sql = "DELETE FROM dbo.settings WHERE skey=@Key";

            // Create command
            var cmd = new SqlCommand
            {
                CommandText = sql
            };

            // Add a parameter to the command
            var para = cmd.Parameters.Add("@Key", SqlDbType.VarChar);
            para.Value = "XXX";

            Assert.DoesNotThrow(() => _db.Exec(cmd));
        }


        /// <summary>
        /// Get a scalar value from database from a plain SQL string (avoid this option due to SQL injection)
        /// </summary>
        [Test]
        public void TestExecWithResultFromSql()
        {

            const string sql = "SELECT [Value] FROM dbo.settings WHERE skey='Company'";

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

            const string sql = "SELECT [Value] FROM dbo.settings WHERE skey=@Key";

            // Create command
            var cmd = new SqlCommand
            {
                CommandText = sql
            };

            // Add a parameter to the command
            var para = cmd.Parameters.Add("@Key", SqlDbType.VarChar);
            para.Value = "Company";

            var result = _db.ExecWithResult(cmd);

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
    }


# About us

Bodoconsult (<http://www.bodoconsult.de>) is a Munich based software development company from Germany.

Robert Leisner is senior software developer at Bodoconsult. See his profile on <http://www.bodoconsult.de/Curriculum_vitae_Robert_Leisner.pdf>.

