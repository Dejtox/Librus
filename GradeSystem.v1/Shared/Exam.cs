using System.ComponentModel.DataAnnotations;

public class Exam
{
    [Required]
    public int ExamID { get; set; }
    [Required]
    public int SubjectID { get; set; }
    [Required]
    public int ClassID { get; set; }
    public Subject Subject { get; set; }
    public Class Class { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}