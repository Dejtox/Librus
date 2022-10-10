using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dziennik.Data;
using Dziennik.Models;

namespace Dziennik.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly Dziennik.Data.SchoolContext _context;

        public IndexModel(Dziennik.Data.SchoolContext context)
        {
            _context = context;
        }

        public string LastNameSort { get; set; }
        public string FirstNameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString)
    {
        // using System;
        LastNameSort = String.IsNullOrEmpty(sortOrder) ? "Lname_desc" : "";
        FirstNameSort = String.IsNullOrEmpty(sortOrder) ? "Fname_desc" : "";

        CurrentFilter = searchString;

        IQueryable<Student> studentsIQ = from s in _context.Student
                                        select s;

        if (!String.IsNullOrEmpty(searchString))
            {
            studentsIQ = studentsIQ.Where(s => s.LastName.Contains(searchString)
                                    || s.FirstName.Contains(searchString));
        }

        switch (sortOrder)
        {
            case "Lname_desc":
                studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                break;
            case "Fname":
                studentsIQ = studentsIQ.OrderBy(s => s.FirstName);
                break;
            case "Fname_desc":
                studentsIQ = studentsIQ.OrderByDescending(s => s.FirstName);
                break;
            default:
                studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                break;
        }

        Student = await studentsIQ.AsNoTracking().ToListAsync();
    }
    }
}
