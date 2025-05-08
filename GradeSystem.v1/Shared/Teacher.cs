using System.ComponentModel.DataAnnotations;

public class Teacher //zmieni³bym nazwe na employee
{
    [Required]
    public int TeacherID { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name cannot be longer than 100 characters. Required minimum length is 2.")]
    public string LastName { get; set; } = string.Empty;
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "First name cannot be longer than 100 characters. Required minimum length is 2.")]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } = string.Empty;
    [Required]
    public int UserID { get; set; }
    public User? User { get; set; }
    public string? Status { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Name => $"{FirstName} {LastName}";

}
