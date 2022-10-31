public class Student
    {
        public int StudentID {get; set; }
        public int ParentID {get; set; }
        public int ClassID {get; set; }

        public string LastName {get; set; } = string.Empty;
        public string FirstName {get; set; } = string.Empty;
        public string Pesel { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public int UserID { get; set; }

        public Parent? Parent {get; set; }
        public Class? Class {get; set; }
        public User? User { get; set; }
}
