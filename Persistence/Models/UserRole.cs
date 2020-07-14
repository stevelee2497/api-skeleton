using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models
{
    [Table("UserRole")]
    public class UserRole : BaseModel
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
