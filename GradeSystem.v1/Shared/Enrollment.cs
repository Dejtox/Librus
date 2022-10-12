
    public class Enrollment
    {

        public int EnrollmentID { get; set; }
        public string SubjectID { get; set; } = String.Empty;
        public string ClassID { get; set; } = String.Empty;
        public DateTime? Date { get; set; }

        public Subject? Subject { get; set; }
        public Class? Class { get; set; }
        
        public ICollection<Attendance>? Attendances { get; set; }
    }
