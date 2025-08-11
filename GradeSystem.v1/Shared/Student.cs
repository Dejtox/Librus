using System.ComponentModel.DataAnnotations;

public class Student
{
    [Required]
    public int StudentID {get; set; }
    [Required]
    public int ParentID {get; set; }
    [Required]
    public int ClassID { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name cannot be longer than 100 characters. Required minimum length is 2.")]
    public string LastName {get; set; } = string.Empty;
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "First name cannot be longer than 100 characters. Required minimum length is 2.")]
    public string FirstName {get; set; } = string.Empty;


    [Required]
    public int UserID { get; set; }
    [Required]
    public int StudentGroup { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Language cannot be longer than 50 characters. Required minimum length is 2.")]
    public string Additional_Language { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public Parent? Parent {get; set; }
    public Class? Class {get; set; }
    public User? User { get; set; }
    public string Name => $"{FirstName} {LastName} {Class?.ClassName}";
    public string FullName => $"{FirstName} {LastName}";

}
//mail i do wywalenia pesel 