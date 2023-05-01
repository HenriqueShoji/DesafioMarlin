namespace DesafioMarlin.Middleware.Exceptions
{
    public class CpfInvalidoException : Exception
    {
        public CpfInvalidoException() { }
        public CpfInvalidoException(string message) : base(message) { }
    }
}
