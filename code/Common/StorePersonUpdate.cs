namespace Common
{
    public class StorePersonUpdate
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Dob { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;
    }
}
