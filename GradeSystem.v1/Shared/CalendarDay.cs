
using System.ComponentModel.DataAnnotations;

public class CalendarDay
{
    [Required]
    public int DayNumber { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public bool IsEmpty { get; set; }
    public List<CalendarEvent> Events { get; set; }
}