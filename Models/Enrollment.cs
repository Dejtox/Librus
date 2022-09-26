using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.Runtime;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System;

namespace Dziennik.Models
{
    public class Enrollment
    {

        public int EnrollmentID { get; set; }
        public string SubjectID { get; set; }
        public string ClassID { get; set; }
        public DateTime Date { get; set; }

        public Subject Subject { get; set; }
        public Class Class { get; set; }
    }
}