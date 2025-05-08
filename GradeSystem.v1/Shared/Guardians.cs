using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Guardians
{
    [Required]
    public int GuardiansID { get; set;}
    [Required]
    public int TeacherID { get; set;}
    [Required]
    public int SchoolTripID { get; set;}
    public Teacher? Teacher { get; set;}
    [JsonIgnore]
    public SchoolTrip? SchoolTrip { get; set;}
}
