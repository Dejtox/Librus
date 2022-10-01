using System.Collections.Generic;  
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;


namespace Dziennik.Models
{
    public class Student
    {
        public int ID {get; set; }
        public int ParentID {get; set; }
        public int ClassID {get; set; }

        public string LastName {get; set; }
        public string FirstName {get; set; }
        public string StudentPesel {get; set; }

        public string Login {get; set; }
        public string Password {get; set; }

        public Parent Parent {get; set; }
        public Class Class {get; set; }

        

    }
}