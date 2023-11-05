namespace Authentication_and_Authorization.Core.Exceptions
{
    public class UserWithEmailAlreadyExistsException : Exception
    {
        public UserWithEmailAlreadyExistsException(string message) : base(message) { }
    }
}
