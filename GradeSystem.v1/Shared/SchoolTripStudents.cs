using System.Text.Json.Serialization;

public class SchoolTripStudents
{
    public int SchoolTripStudentsID { get; set; }
    public int StudentID { get; set; }
    public Student? Student { get; set; }
    public int SchoolTripID { get; set; }
    [JsonIgnore]
    public SchoolTrip? SchoolTrip { get; set; }
}
