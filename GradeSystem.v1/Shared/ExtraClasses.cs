using System.ComponentModel.DataAnnotations;

public class ExtraClasses
{
    [Required]
    public int ExtraClassesID { get; set; }
    [Required]
    public int TeacherID { get; set; }
    [Required]
    public int CurrentCapasity { get; set; } = 0;
    [Required]
    public int MaxCapasity { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Class name cannot be longer than 100 characters. Required minimum length is 2.")]
    public string ExtraClassesName { get; set; } = string.Empty;
    [Required]
    [StringLength(1000, MinimumLength = 2, ErrorMessage = "Class name cannot be longer than 1000 characters. Required minimum length is 2.")]
    public string ExtraClassesDescription { get; set; } = string.Empty;
    [Required]
    public DateTime ExtraClassesDate { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Class room name cannot be longer than 50 characters. Required minimum length is 1.")]
    public string ClassRoom { get; set; } = string.Empty;

    public Teacher? Teacher { get; set; }
}


