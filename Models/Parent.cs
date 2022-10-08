using System.Collections.Generic;  
using System.ComponentModel.DataAnnotations;

namespace Dziennik.Models
{
    public class Parent
    {
        public int ParentID {get; set; }
        public string LastName {get; set; }
        public string FirstName {get; set; }
        public string Email {get; set; }
        public string PhoneNumber {get; set; }
        public string Login {get; set; }
        public string Password {get; set; }
        public string ParentRole {get; set; } = "Parent";
        public ICollection<Student> Students {get; set; }
    }
}