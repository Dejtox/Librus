
using System.ComponentModel.DataAnnotations;

public class GradeNumber
{
    [Required]
    public int GradeNumberID { get; set; }
    [Required]
    public float gradenumber { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Grade name cannot be longer than 50 characters. Required minimum length is 2.")]
    public string GradeName { get; set; } = string.Empty;
}

