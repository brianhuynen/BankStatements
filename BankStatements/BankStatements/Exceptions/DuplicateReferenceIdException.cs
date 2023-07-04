namespace BankStatements.Exceptions
{
    public class DuplicateReferenceIdException : Exception
    {
        public DuplicateReferenceIdException() {}

        public DuplicateReferenceIdException(string message) : base(message) { }
    }
}
