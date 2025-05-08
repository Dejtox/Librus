using System.ComponentModel.DataAnnotations;

public class User
{
    [Required]
    public int UserID { get; set; }
    [Required]
    [StringLength(25, MinimumLength = 4, ErrorMessage = "Last name cannot be longer than 25 characters. Required minimum length is 4.")]
    public string Login { get; set; } = string.Empty;
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    [Required]
    public string PasswordSalt { get; set; } = string.Empty;
    //public string UserRole { get; set; } = string.Empty;
    public List<Roles>? Roles { get; set; }
    public string? Roless => Roles != null
    ? string.Join(",", Roles.Select(c => c.Role))
    : null;
}
