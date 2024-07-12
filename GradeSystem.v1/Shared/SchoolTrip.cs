using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SchoolTrip
{
    public int SchoolTripID { get; set; }

    //[Required(ErrorMessage = "Trip name is required.")]
    //[StringLength(100, ErrorMessage = "Trip name cannot be longer than 100 characters.")]
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }= DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public List<SchoolTripTeachers>? SchoolTripTeachers { get; set; }
    public List<SchoolTripClasses>? SchoolTripClasses { get; set; }
}

