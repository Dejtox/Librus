
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Parent
{
    [Required]
    public int ParentID { get; set; }
    public int? LastSelectedChildId { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name cannot be longer than 100 characters. Required minimum length is 2.")]
    public string LastName { get; set; } = string.Empty;
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "First name cannot be longer than 100 characters. Required minimum length is 2.")]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } = string.Empty;
    [Required]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public int UserID { get; set; }
    public User? User { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
