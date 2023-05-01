namespace DesafioMarlin.Middleware.Exceptions
{
    public class EmailInvalidoException : Exception
    {
        public EmailInvalidoException() { }
        public EmailInvalidoException(string message) : base(message) { }
    }
}
