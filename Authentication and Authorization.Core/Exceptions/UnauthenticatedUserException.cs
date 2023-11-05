namespace Authentication_and_Authorization.Core.Exceptions
{
    public class UnauthenticatedUserException : Exception
    {
        public UnauthenticatedUserException(string message) : base(message) { }
    }
}
