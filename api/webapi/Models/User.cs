using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPM.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int user_id { get; set; }

        [StringLength(255)]
        public string first_name { get; set; } = string.Empty;

        [StringLength(255)]
        public string last_name { get; set; } = string.Empty;

        [StringLength(255)]
        public string username { get; set; } = string.Empty;
        //These three saved for later
        /*
        [StringLength(50)]
        public string email { get; set; } = string.Empty;

        [StringLength(50)]
        public string password { get; set; } = string.Empty;

        [StringLength(50)]
        public string phone_number { get; set; } = string.Empty;
        */

        public bool is_admin { get; set; }

    }
}
