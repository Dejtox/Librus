
    public class Subject
    {
        public int SubjectID { get; set; }
        public int TeacherID { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public Teacher? Teacher { get; set; }

    }
