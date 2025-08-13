public class DayOff
{
    public int DayOffID { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string? Description { get; set; } = string.Empty;
}
