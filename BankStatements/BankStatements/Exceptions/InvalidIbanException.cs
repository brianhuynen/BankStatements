namespace BankStatements.Exceptions
{
    public class InvalidIbanException : Exception
    {
        public InvalidIbanException() { }
        public InvalidIbanException(string message) : base(message){ }
    }
}
