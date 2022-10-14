    public class Teacher
    {
        public int TeacherID { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;

        public string Login {get; set; } = String.Empty;
        public string Password {get; set; } = String.Empty;
        public string TeacherRole {get; set; } = String.Empty;

        public ICollection<Subject>? Subjects { get; set; }
    }
