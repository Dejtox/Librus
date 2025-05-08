
using System.ComponentModel.DataAnnotations;

public class Class
{
    [Required]
    public int ClassID { get; set; }
    [Required]
    public int? TeacherID { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Class name cannot be longer than 50 characters. Required minimum length is 2.")]
    public string ClassName { get; set; } = string.Empty;
    public Teacher? Teacher { get; set; }
    [Required]
    public bool VirtualClass { get; set; }
}