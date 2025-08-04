using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPM.Models
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public int event_id { get; set; }
        public int user_id { get; set; }
        public int park_id { get; set; }

        [StringLength(255)]
        public int registration_id { get; set; }
        [StringLength(255)]
        public string event_name { get; set; } = string.Empty;
        [StringLength(4000)]
        public string event_desc { get; set; } = string.Empty;
        public DateTime event_start_date { get; set; }
        public DateTime event_end_date { get; set; }
        public TimeSpan event_start_time { get; set; }
        public TimeSpan event_end_time { get; set; }
        [NotMapped]
        public bool is_public_event { get; set; }
        [NotMapped]
        public int num_of_attendees { get; set; }
        [ForeignKey(nameof(user_id))]
        public virtual required User? User { get; set; }
        [ForeignKey(nameof(park_id))]
        public virtual required Park? Park { get; set; }
        [ForeignKey(nameof(registration_id))]
        public virtual required Registration? Registration {get; set; }
    }
}