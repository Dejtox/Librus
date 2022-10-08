using System.Collections.Generic;  
using System.ComponentModel.DataAnnotations;

namespace Dziennik.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string Login {get; set; }
        public string Password {get; set; }
        public string TeacherRole {get; set; }= "Teacher";

        public ICollection<Subject> Subjects { get; set; }
        public Class Class { get; set; }
    }
}