namespace Authentication_and_Authorization.Models
{
    public class JWTConfig
    {
        public const string SectionName = "JwtSecurityToken";
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
