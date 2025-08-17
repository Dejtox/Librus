using System.ComponentModel.DataAnnotations;

public class Exam
{
    [Required]
    public int ExamID { get; set; }
    [Required]
    public int SubjectID { get; set; }
    [Required]
    public int ClassID { get; set; }
    public Subject? Subject { get; set; }
    public Class? Class { get; set; }
    [Required]
    [StringLength(500, MinimumLength = 2, ErrorMessage = "Subject name cannot be longer than 500 characters. Required minimum length is 2.")]
    public string Description { get; set; } = string.Empty;
    [Required]
    public DateTime Date { get; set; }
}