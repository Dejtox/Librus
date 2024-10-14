
    public class Parent
    {
        public int ParentID { get; set; }
        public int? LastSelectedChildId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int UserID { get; set; }
        public User? User { get; set; }
      
    }
