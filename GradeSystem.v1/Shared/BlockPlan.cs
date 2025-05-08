using System.ComponentModel.DataAnnotations;

public class BlockPlan
{
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Block name cannot be longer than 100 characters. Required minimum length is 3.")]
    public string Name { get; set; }
    [Required]
    public DateTime Starthour { get; set; }
    [Required]
    public DateTime Endhour { get; set; }
    [Required]
    public bool IsEmpty { get; set; }

    public Enrollment? Lesson { get; set; }
    public ExtraClasses? AdditionalAct { get; set; }
}