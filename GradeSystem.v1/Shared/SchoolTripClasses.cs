using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class SchoolTripClasses
{
    [Required]
    public int SchoolTripClassesID { get; set;}
    [Required]
    public int ClassID { get; set;}
    [Required]
    public int SchoolTripID { get; set;}
    [JsonIgnore]
    public SchoolTrip? SchoolTrip { get; set;}
    public Class? Class { get; set;}
}
