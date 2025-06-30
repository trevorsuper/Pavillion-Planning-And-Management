using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.DTOs
{
    public class RegistrationDTO
    {
        public int registration_id { get; set; }
        public int user_id { get; set; }
        public int park_id { get; set; }
        public int event_id { get; set; }
        public int pavillion { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public bool is_approved { get; set; }
    }
}
