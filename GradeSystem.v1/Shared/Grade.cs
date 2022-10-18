
    public class Grade
    {
        public int GradeID {get; set; }
        public int StudentID { get; set; }       
        public int SubjectID {get; set; }
        public int GradeNumber {get; set; }
        public int GradeWeight {get; set; }
        public string Description { get; set; } = string.Empty;


        public Student? Student {get; set; }
        public Subject? Subject {get; set; }
        
    }
