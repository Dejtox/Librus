using System.Text.Json.Serialization;

public class SchoolTripTeachers
{
    public int SchoolTripTeachersID { get; set;}

    public int TeacherID { get; set;}
    public int SchoolTripID { get; set;}
    public Teacher? Teacher { get; set;}
    [JsonIgnore]
    public SchoolTrip? SchoolTrip { get; set;}
}
