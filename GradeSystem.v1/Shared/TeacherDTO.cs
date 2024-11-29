public class TeacherDTO
{
    public int ID { get; set; }
    public string Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Duration { get; set; }
    public int AbsentHours { get; set; }
    public int AssignHours { get; set; }
}