
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int SubjectID { get; set; } 
        public int ClassID { get; set; } 
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string ClassRoom { get; set; } = string.Empty;
        public bool Replacement { get; set; } = false;
        public int Substitute_SubjectID { get; set; }
        public string Link_To_Materials { get; set; } = string.Empty;
        public Subject? Subject { get; set; }
        public Class? Class { get; set; }
        
    }
