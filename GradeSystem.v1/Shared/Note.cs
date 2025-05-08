using System.ComponentModel.DataAnnotations;

public class Note
{
    [Required]
    public int NoteID { get; set; }
    [Required]
    public int TeacherID {  get; set; }
    public int? Position { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Title cannot be longer than 100 characters. Required minimum length is 2.")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Category cannot be longer than 100 characters. Required minimum length is 2.")]
    public string Category { get; set; } = string.Empty;
    [Required]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Content cannot be longer than 1000 characters. Required minimum length is 1.")]
    public string Content { get; set; } = string.Empty;
    [Required]
    public string Color { get; set; } = string.Empty;

}