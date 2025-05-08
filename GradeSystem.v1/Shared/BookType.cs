using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


public class BookType 
{
    [Required]
    public int BookTypeID { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Title cannot be longer than 100 characters. Required minimum length is 2.")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Author cannot be longer than 100 characters. Required minimum length is 2.")]
    public string Author { get; set; } = string.Empty;
    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Edition cannot be longer than 100 characters. Required minimum length is 1.")]
    public string Edition { get; set; } = string.Empty;
    [StringLength(1000, ErrorMessage = "Descriptions cannot be longer than 1000 characters.")]
    public string? Description { get; set; } = string.Empty;
    [Required]
    public string Cover { get; set; } = string.Empty;
    [Required]
    public int AmountOfBooks { get; set; }

    [NotMapped]
    public IList<int>? BookIds { get; set; } = new List<int>();


    public List<Book>? Books { get; set; } = new List<Book>();
}

