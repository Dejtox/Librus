using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dziennik.Data;
using Dziennik.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Dziennik.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly Dziennik.Data.SchoolContext _context;

        public IndexModel(Dziennik.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Student != null)
            {
                Student = await _context.Student
                .Include(s => s.Class)
                .Include(s => s.Parent).ToListAsync();
            }
        }
    }
}
