namespace WebApiArchutecture.Application.DTOs.UserDto
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        
        public string Role { get; set; } = "User";
        public string Telegram_Tag { get; set; }

        
    }
}
