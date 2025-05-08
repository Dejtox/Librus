
using System.ComponentModel.DataAnnotations;

public class Subject
{
    [Required]
    public int SubjectID { get; set; }
    [Required]
    public int? TeacherID { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2,ErrorMessage = "Subject name cannot be longer than 50 characters. Required minimum length is 2.")]
    public string SubjectName { get; set; } = string.Empty;
    public Teacher? Teacher { get; set; }
}
