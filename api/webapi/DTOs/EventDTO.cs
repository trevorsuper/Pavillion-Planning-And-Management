using System.ComponentModel.DataAnnotations;

namespace webapi.DTOs
{
    public class EventDTO
    {
        public int event_id { get; set; }
        public string event_name { get; set; }
        public string event_description { get; set; }
        public DateTime event_start_date { get; set; }
        public DateTime event_end_date { get; set; }
        public DateTime event_start_time { get; set; }
        public DateTime event_end_time { get; set; }
    }
}
