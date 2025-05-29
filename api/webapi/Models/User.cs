using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPM.Models
{
    [Table("Users")]
    public class User
    {
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
        [Key]
        public int ID { get; set; }
    }
}
