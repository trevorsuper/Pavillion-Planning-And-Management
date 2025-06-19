using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PPM.Models
{
    [Table("Parks")]
    public class Park
    {
        [Key]
        public int park_id { get; set; }

        [StringLength(255)]
        public string park_name { get; set; } = string.Empty;

        [StringLength(255)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? geolocation { get; set; } = string.Empty;

        [StringLength(255)]
        public string street_address { get; set; } = string.Empty;

        [StringLength(20)]
        public string city { get; set; } = string.Empty;

        [StringLength(20)]
        public string state { get; set; } = string.Empty;

        [StringLength(10)]
        public string zipcode { get; set; } = string.Empty;
        public int number_of_pavillions { get; set; }
        public int acres { get; set; }
        public int play_structures { get; set; }
        public int trails { get; set; }
        public int baseball_fields { get; set; }
        public int disc_golf_courses { get; set; }
        public int volleyball_courts { get; set; }
        public int fishing_spots { get; set; }
        public int soccer_fields { get; set; }
    }
}
