    public class Student
    {
        public int StudentID {get; set; }
        public int ParentID {get; set; }
        public int ClassID {get; set; }

        public string LastName {get; set; } = string.Empty;
        public string FirstName {get; set; } = String.Empty;

        public string Login {get; set; } = String.Empty;
        public string Password {get; set; } = String.Empty;
        public string StudentRole {get; set; } = "Student";

        public Parent? Parent {get; set; }
        public Class? Class {get; set; }
        public ICollection<Grade>? Grades {get; set; }
        public ICollection<Attendance>? Attendances { get; set; }

        

    }
