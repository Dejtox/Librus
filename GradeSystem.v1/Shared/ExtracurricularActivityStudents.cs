using System.Text.Json.Serialization;

public class ExtracurricularActivityStudents
{
    public int ExtracurricularActivityStudentsID { get; set; }
    public int StudentID { get; set; }
    public Student? Student { get; set; }
    public int ExtracurricularActivityID { get; set; }
    [JsonIgnore]
    public ExtracurricularActivity? ExtracurricularActivity { get; set; }
    public bool IsReserve { get; set; } = false;
    public DateTime JoinedAt { get; set; } 
}