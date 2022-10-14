
    public class Subject
    {
        public int SubjectID { get; set; }
        public int TeacherID { get; set; }
        public string SubjectName { get; set; } = String.Empty;
        public Teacher? Teacher { get; set; }

    }
