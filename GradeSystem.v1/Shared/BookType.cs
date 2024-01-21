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
    public int BookTypeID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;

    public string Edition { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public string Cover { get; set; } = string.Empty;

    public int AmountOfBooks { get; set; }

    [NotMapped]
    public IList<int>? BookIds { get; set; } = new List<int>();


    public List<Book>? Books { get; set; } = new List<Book>();
}

