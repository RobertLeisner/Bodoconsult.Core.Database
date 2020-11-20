namespace Bodoconsult.Core.Database
{
    public class DbConnException : System.Exception
    {
        public DbConnErrorCode Erc;

        public DbConnException(string message, DbConnErrorCode e)
            : base(message)
        {
            Erc = e;
        }
    }
}