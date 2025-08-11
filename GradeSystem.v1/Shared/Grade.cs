
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography.X509Certificates;

public class Grade
{
    [Required]
    public int GradeID { get; set; }
    [Required]
    public int StudentID { get; set; }
    [Required]
    public int SubjectID { get; set; }
    public int Semester { get; set; }

    public GradeNumber? Gradenumber { get; set; }
    
    public GradeType? Gradetype { get; set; }
    [Required]
    public int GradeNumberID { get; set; }
    [Required]
    public int GradeTypeId { get; set; }
    [Required]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Grade name cannot be longer than 1000 characters. Required minimum length is 1.")]
    public string Description { get; set; } = string.Empty;
    [Required]
    public int GradeWeight { get; set; }
    public Student? Student { get; set; }
    public Subject? Subject { get; set; }
    [Required]
    public DateTime DateOfGrading { get; set; }
}
