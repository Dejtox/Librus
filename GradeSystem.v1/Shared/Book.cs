﻿public class Book
{
    public int BookID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public bool IsBorowed { get; set; }
    public bool IsBoocked { get; set; }
    public int QRCode { get; set; }

    public int? StudentId { get; set; }
    public Student? Student { get; set; }
}


