
    public class Attendance
    {
    public int AttendanceID { get; set; }
    public int EnrollmentID { get; set; }
    public int StudentID { get; set; }
    public string Description { get; set; } = string.Empty;
    public Student? Student { get; set; }
    public Enrollment? Enrollment { get; set; }
    public DateTime CreatoinDate { get; set; }
    
    }
