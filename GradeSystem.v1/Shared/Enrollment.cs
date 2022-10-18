
    public class Enrollment
    {

        public int EnrollmentID { get; set; }
        public string SubjectID { get; set; } = string.Empty;
        public string ClassID { get; set; } = string.Empty;
        public DateTime? Date { get; set; }

        public Subject? Subject { get; set; }
        public Class? Class { get; set; }
        
    }
