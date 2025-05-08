
using System.ComponentModel.DataAnnotations;

public class CalendarEvent
{
    [Required]
    public int CalendarEventID { get; set; }
    [Required]
    public int ClassID { get; set; }
    [Required]
    public int? SubjectID   { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Title cannot be longer than 100 characters. Required minimum length is 2.")]
    public string Title { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Color cannot be longer than 100 characters. Required minimum length is 2.")]
    public string Color { get; set; }
    [Required]
    [StringLength(1000, MinimumLength =3,ErrorMessage = "Description cannot be longer than 1000 characters. Required minimum length is 3.")]
    public string Description { get; set; }
    public Class? Class { get; set; }
}

