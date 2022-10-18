    public class Teacher
    {
        public int TeacherID { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Login {get; set; } = string.Empty;
        public string Password {get; set; } = string.Empty;
        public string TeacherRole {get; set; } = string.Empty;

        public ICollection<Subject>? Subjects { get; set; }
    }
