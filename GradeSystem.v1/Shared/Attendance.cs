
using System.ComponentModel.DataAnnotations;

public class Attendance
{
    [Required]
    public int AttendanceID { get; set; }
    [Required]
    public int EnrollmentID { get; set; }
    [Required]
    public int StudentID { get; set; }
    [Required]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Description cannot be longer than 1000 characters. Required minimum length is 1.")]
    public string Description { get; set; } = string.Empty;
    public Student? Student { get; set; }
    public Enrollment? Enrollment { get; set; }
    [Required]
    public DateTime CreatoinDate { get; set; }    
}
