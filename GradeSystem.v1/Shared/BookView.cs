using System.ComponentModel.DataAnnotations;

public class BookView
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Title cannot be longer than 100 characters. Required minimum length is 2.")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Author cannot be longer than 100 characters. Required minimum length is 2.")]
    public string Author { get; set; } = string.Empty;
    [Required]
    public int Amount { get; set; } = 0;
    [Required]
    public string Img { get; set; } = string.Empty;


    public IList<int> Ids { get; set; }

}