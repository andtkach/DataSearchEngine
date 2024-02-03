namespace Common
{
    public interface IPerson
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EmailText { get; set; }
        public string Country { get; set; }
        public string Dob { get; set; }
        public string Bio { get; set; }
    }
}
