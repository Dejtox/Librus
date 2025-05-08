using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class SchoolTripStudents
{
    [Required]
    public int SchoolTripStudentsID { get; set; }
    [Required]
    public int StudentID { get; set; }
    public Student? Student { get; set; }
    [Required]
    public int SchoolTripID { get; set; }
    [JsonIgnore]
    public SchoolTrip? SchoolTrip { get; set; }
}
