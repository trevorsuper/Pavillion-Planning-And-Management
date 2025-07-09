namespace PPM.Models.DTOs
{
    public class AuthResponseDTO
    {
        public string token { get; set; }
        public UserDTO User {get;set;}
    }
}
