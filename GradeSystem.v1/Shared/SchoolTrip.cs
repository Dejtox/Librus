using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SchoolTrip
{
    public int SchoolTripID { get; set; }
    public string Name { get; set; }
    public string Purpose { get; set; }
    public string Location { get; set; }
    public string AdditionalInfo {  get; set; }
    public string Transportation { get; set; }
    public int TripLeaderID { get; set; }
    public Teacher? TripLeader { get; set; }
    public string AddedBy { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }= DateTime.Now;
    public List<Guardians>? Guardians { get; set; }
    public string NonSchoolGuardians { get; set; }
    public List<SchoolTripClasses>? Classes { get; set; }
    public List<SchoolTripStudents>? Students { get; set; }
    public int Ppl => Students.Count();
    public string Duration => $"{StartDate.ToString("dd.MM.yyyy")}-{EndDate.ToString("dd.MM.yyyy")}";
}

