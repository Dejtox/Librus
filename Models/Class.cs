using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dziennik.Models
{
    public class Class
    {
        public int ClassID { get; set; }
        public int TeacherID { get; set; }
        public string ClassName { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}