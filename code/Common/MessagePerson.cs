namespace Common
{
    public class MessagePerson: IPerson
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EmailText { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Dob { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
    }
}
