namespace Authentication_and_Authorization.Data.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }

    public enum UserType
    {
        Admin = 1, Customer 
    }
}
