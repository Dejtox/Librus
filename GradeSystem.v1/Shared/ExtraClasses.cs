public class ExtraClasses
{
    public int ExtraClassesID { get; set; }
    public int TeacherID { get; set; }
    public int MaxCapasity { get; set; }
    public string ExtraClassesName { get; set; } = string.Empty;
    public string ExtraClassesDescription { get; set; } = string.Empty;
    public DateTime ExtraClassesDate { get; set; }
    public string ClassRoom { get; set; } = string.Empty;

    public Teacher? Teacher { get; set; }
}


