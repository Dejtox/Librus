
using System.ComponentModel.DataAnnotations;

public class LessonsHours
{
    [Required]
    public int ID { get; set; }
    [Required]
    public int LessonNo { get; set; }
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public DateTime End { get; set; }
}

