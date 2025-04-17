namespace TicketToCode.Services
{
    public class AuthState
    {
        public string? Username { get; set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(Username);

        // Constructor for easy initialization
        public AuthState(string? username = null)
        {
            Username = username;
        }
    }
}
