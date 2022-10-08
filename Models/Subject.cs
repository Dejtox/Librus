using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;  

namespace Dziennik.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public int TeacherID { get; set; }
        public string SubjectName { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Grade> Grades { get; set; }

    }
}