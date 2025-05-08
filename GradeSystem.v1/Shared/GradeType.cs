
using System.ComponentModel.DataAnnotations;

public class GradeType
{
    [Required]
    public int GradeTypeId { get; set; }
    [Required]
    public float GradeWeight { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Grade name cannot be longer than 50 characters. Required minimum length is 2.")]
    public string GradeTypeName { get; set; }
    [Required]
    public string color { get; set; }

}
