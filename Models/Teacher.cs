using System.Collections.Generic;  
using System.ComponentModel.DataAnnotations;

namespace Dziennik.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string TeacherLastName { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherPhoneNumber { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherPesel { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public Class Class { get; set; }
    }
}