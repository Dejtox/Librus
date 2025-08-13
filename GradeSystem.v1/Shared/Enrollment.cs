
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Enrollment
{
    [Required]
    public int EnrollmentID { get; set; }
    [Required]
    public int? SubjectID { get; set; }
    [Required]
    public int? ClassID { get; set; }
    public  int? SubEnrollmentID { get; set; }

    public bool? Issubbstitut { get; set; }

    public Enrollment? SubEnrollment { get; set; }
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Class room name cannot be longer than 50 characters. Required minimum length is 1.")]
    public string ClassRoom { get; set; } = string.Empty;
    //[Required]
    //[StringLength(100, MinimumLength = 1, ErrorMessage = "Lesson topic cannot be longer than 100 characters. Required minimum length is 1.")]
    //public string LessonTopic { get; set; } = string.Empty;

    public Subject? Subject { get; set; }
    public Class? Class { get; set; }

    public string? Description { get; set; }

}
