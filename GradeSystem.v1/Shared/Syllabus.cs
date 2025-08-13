
using System.ComponentModel.DataAnnotations;

public class Syllabus
{
    [Required]
    public int SyllabusID { get; set; }
    [Required]
    public int? SubjectID { get; set; }
    [Required]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Class name cannot be longer than 200 characters. Required minimum length is 1.")]
    public string SyllabusTopic { get; set; } = string.Empty;
}