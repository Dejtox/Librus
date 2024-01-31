using System.ComponentModel.DataAnnotations.Schema;

public class SchoolTrip
{
    public int SchoolTripID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }= DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public List<SchoolTripTeachers>? SchoolTripTeachers { get; set; }
    public List<SchoolTripClasses>? SchoolTripClasses { get; set; }
}

