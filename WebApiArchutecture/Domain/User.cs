namespace WebApiArchutecture.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User";
        public string Email { get; set; }
        public string Telegram_Tag { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
        public bool IsRevoked { get; set; } = false;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
