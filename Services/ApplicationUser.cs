namespace Esercitazione.Services
{
    public class ApplicationUser
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
        public string? FriendlyName { get; set; }
    }
}
