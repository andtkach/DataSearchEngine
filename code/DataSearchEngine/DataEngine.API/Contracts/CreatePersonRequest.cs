using System.ComponentModel.DataAnnotations;

namespace DataEngine.API.Contracts;

public class CreatePersonRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;
    
    [Required]
    public string Dob { get; set; } = string.Empty;
    
    public string Bio { get; set; } = string.Empty;
    
    public string ProfileUrl { get; set; } = string.Empty;
}