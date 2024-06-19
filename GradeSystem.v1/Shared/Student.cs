using System.ComponentModel.DataAnnotations.Schema;

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
        public int StudentGroup { get; set; }
        public string Additional_Language { get; set; } = string.Empty;
        public string StringClassIdList { get; set; } = string.Empty;

        [NotMapped]
         public IList<int> ClassIdList { get; set; } = new List<int>();


        [NotMapped]
        public IList<Class>? ClassList { get; set; } = new List<Class>();

        public Parent? Parent {get; set; }
        public User? User { get; set; }
}
