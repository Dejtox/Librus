
using System.ComponentModel.DataAnnotations;

public class ExtraClassesList
{
    [Required]
    public int ExtraClassesListID { get; set; }
    [Required]
    public int ExtraClassesID { get; set; }
    [Required]
    public int StudentID { get; set; }
    [Required]
    public DateTime DateTime { get; set; }= DateTime.Now;
    public Student? Student { get; set; }
    public ExtraClasses? ExtraClasses { get; set; }
}
