
public class ExtraClassesList
{
    public int ExtraClassesListID { get; set; }
    public int ExtraClassesID { get; set; }
    public int StudentID { get; set; }
    public DateTime DateTime { get; set; }= DateTime.Now;
    public Student? Student { get; set; }
    public ExtraClasses? ExtraClasses { get; set; }
}
