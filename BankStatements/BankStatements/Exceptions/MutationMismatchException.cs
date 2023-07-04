namespace BankStatements.Exceptions
{
    public class MutationMismatchException : Exception
    {
        public MutationMismatchException() { }
        public MutationMismatchException(string message) : base(message) { }
    }
}
