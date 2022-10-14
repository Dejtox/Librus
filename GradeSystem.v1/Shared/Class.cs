
    public class Class
    {
        public int ClassID { get; set; }
        public int TeacherID { get; set; }
        public string ClassName { get; set; } = String.Empty;

        public Teacher? Teacher { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }

    }
