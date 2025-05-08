using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SchoolTrip
{
    [Required]
    public int SchoolTripID { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name cannot be longer than 100 characters. Required minimum length is 2.")]
    public string Name { get; set; }
    [Required]
    [StringLength(500, MinimumLength = 2, ErrorMessage = "Description cannot be longer than 500 characters. Required minimum length is 2.")]
    public string Purpose { get; set; }
    [Required]
    [StringLength(500, MinimumLength = 2, ErrorMessage = "Location cannot be longer than 500 characters. Required minimum length is 2.")]
    public string Location { get; set; }
    [Required]
    [StringLength(500, MinimumLength = 2, ErrorMessage = "Additional info cannot be longer than 500 characters. Required minimum length is 2.")]
    public string AdditionalInfo {  get; set; }
    [Required]
    [StringLength(500, MinimumLength = 2, ErrorMessage = "Transportation cannot be longer than 500 characters. Required minimum length is 2.")]
    public string Transportation { get; set; }
    [Required]
    public int TripLeaderID { get; set; }
    public Teacher? TripLeader { get; set; }
    [Required]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Added by cannot be longer than 200 characters. Required minimum length is 2.")]
    public string AddedBy { get; set; }
    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;
    [Required]
    public DateTime EndDate { get; set; }= DateTime.Now;
    public List<Guardians>? Guardians { get; set; }
    [Required]
    [StringLength(3000, MinimumLength = 2, ErrorMessage = "Non-school guardians cannot be longer than 3000 characters. Required minimum length is 2.")]
    public string NonSchoolGuardians { get; set; }
    public List<SchoolTripClasses>? Classes { get; set; }
    public List<SchoolTripStudents>? Students { get; set; }
    public int? Ppl => Students?.Count();
    public string Duration => $"{StartDate.ToString("dd.MM.yyyy")}-{EndDate.ToString("dd.MM.yyyy")}";
    public string? Classess => Classes != null
    ? string.Join(",", Classes.Select(c => c.Class?.ClassName))
    : null;
}

