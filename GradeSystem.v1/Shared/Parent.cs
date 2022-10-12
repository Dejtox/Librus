
    public class Parent
    {
        public int ParentID {get; set; }
        public string LastName { get; set; } = String.Empty;
        public string FirstName {get; set; } = String.Empty;
        public string Email {get; set; } = String.Empty;
        public string PhoneNumber {get; set; } = String.Empty;
        public string Login {get; set; } = String.Empty;
        public string Password {get; set; } = String.Empty;
        public string ParentRole {get; set; } = String.Empty;
        public ICollection<Student>? Students {get; set; }
    }
