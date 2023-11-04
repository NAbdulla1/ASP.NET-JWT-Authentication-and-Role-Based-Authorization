using System.ComponentModel.DataAnnotations;

namespace Authentication_and_Authorization.Data.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public long Id { get; set; }
    }
}
