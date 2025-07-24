namespace PPM.Models.DTOs
{
    public class RegistrationDTO
    {
        public int registration_id { get; set; }
        public int user_id { get; set; }
        public int park_id { get; set; }
        public string? requested_park { get; set; }
        public int pavillion { get; set; }
        public DateTime registration_date { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public bool is_approved { get; set; }
        public UserDTO? User { get; set; }
        public ParkDTO? Park { get; set; }
    }
}
