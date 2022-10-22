
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int SubjectID { get; set; } 
        public int ClassID { get; set; } 
        public DateTime? Date { get; set; }

        public Subject? Subject { get; set; }
        public Class? Class { get; set; }
        
    }
