
    public class Attendance
    {
        public int AttendanceID { get; set; }
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public string Description { get; set; } = string.Empty;
        public ICollection<Student>? Students { get; set; }
        public Enrollment? Enrollment { get; set; }

    }
