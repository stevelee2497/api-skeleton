using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        [Required]
        public DateTime UpdatedTime { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
