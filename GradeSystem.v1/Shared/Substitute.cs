public class Substitute
{
    public int SubstituteID { get; set;}
    public int SubjectID { get; set;}
    public int ClassID { get; set;}
    public DateTime StartDate { get; set;}=DateTime.Now;
    public DateTime EndDate { get; set;}
    public string ClassRoom { get; set;}=string.Empty;
    public string Status { get; set;}=string.Empty;
    public Subject? Subject { get; set; }
    public Class? Class { get; set; }
}
