using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Book
{
    [Required]
    public int BookID { get; set; }
    [Required]
    public bool IsBorowed { get; set; }
    [Required]
    public bool IsBoocked { get; set; }
    [Required]
    public int QRCode { get; set; }
    public int? StudentId { get; set; }
    public Student? Student { get; set; }

    public DateTime? BorowingDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public DateTime? ReservationDate { get; set; }
    public int? BookTypeID { get; set; }

    [JsonIgnore]
    public BookType? BookType { get; set; } 
}


