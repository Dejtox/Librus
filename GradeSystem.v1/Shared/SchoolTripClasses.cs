using System.Text.Json.Serialization;

public class SchoolTripClasses
{
    public int SchoolTripClassesID { get; set;}
    public int ClassID { get; set;}
    public int SchoolTripID { get; set;}
    [JsonIgnore]
    public SchoolTrip? SchoolTrip { get; set;}
    public Class? Class { get; set;}
}
