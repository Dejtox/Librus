
using System.Data;
using System.Security.Cryptography.X509Certificates;

public class Grade
{
    public int GradeID { get; set; }
    public int StudentID { get; set; }
    public int SubjectID { get; set; }

    public GradeNumber? Gradenumber { get; set; }
    
    public GradeType? Gradetype { get; set; } 

    public int GradeNumberID { get; set; }
    public int GradeTypeId { get; set; }
    public int GradeWeight { get; set; }
    public string Description { get; set; } = string.Empty;


    public Student? Student { get; set; }
    public Subject? Subject { get; set; }

    public DateTime DateOfGrading { get; set; }
}
