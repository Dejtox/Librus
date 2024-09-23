using System.Text.Json.Serialization;

public class Guardians
{
    public int GuardiansID { get; set;}

    public int TeacherID { get; set;}
    public int SchoolTripID { get; set;}
    public Teacher? Teacher { get; set;}
    [JsonIgnore]
    public SchoolTrip? SchoolTrip { get; set;}
}
