using System.ComponentModel.DataAnnotations;

public class DayOff
{
    [Required]
    public int DayOffID { get; set; }
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;
    [Required]
    [StringLength(500, MinimumLength = 2, ErrorMessage = "Description cannot be longer than 500 characters. Required minimum length is 2.")]
    public string Description { get; set; } = string.Empty;
}
