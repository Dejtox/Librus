public class ExtracurricularActivity
{
    public int ExtracurricularActivityID { get; set; }
    public int TeacherID { get; set; }
    public Teacher? Teacher { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public string Location { get; set; } = string.Empty;
    public int MaxParticipants { get; set; }
    public List<ExtracurricularActivityStudents>? Participants { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public string Duration => $"{StartDate.ToString("dd.MM.yyyy.HH.mm")}-{EndDate.ToString("dd.MM.yyyy.HH.mm")}";
    public int? SlotsLeft => MaxParticipants - Participants?.Where(p => !p.IsReserve).Count();
}