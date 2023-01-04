
public class CalendarEvent
{
    public int CalendarEventID { get; set; }
    public int ClassID { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
    public Class? Class { get; set; }
}

