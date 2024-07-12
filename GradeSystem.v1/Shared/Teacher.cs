public class Teacher //zmieni³bym nazwe na employee
{
    public int TeacherID { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int UserID { get; set; }
    public User? User { get; set; }
    public string? Status { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Name => $"{FirstName} {LastName}";
}
