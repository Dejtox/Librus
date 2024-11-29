
using System.Text.Json.Serialization;

public class Enrollment
{
    public int EnrollmentID { get; set; }
    public int SubjectID { get; set; }
    public int ClassID { get; set; }
    public  int? SubEnrollmentID { get; set; }
    
    public Enrollment? SubEnrollment { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public string ClassRoom { get; set; } = string.Empty;
    public Subject? Subject { get; set; }
    public Class? Class { get; set; }

}
