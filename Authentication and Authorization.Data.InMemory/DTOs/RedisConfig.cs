namespace Authentication_and_Authorization.Data.InMemory.DTOs
{
    public class RedisConfig
    {
        public const string SectionName = "Redis";

        public string Host {  get; set; }
        public int Port { get; set; }
    }
}
