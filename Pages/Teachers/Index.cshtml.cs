using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dziennik.Data;
using Dziennik.Models;

namespace Dziennik.Pages.Teachers
{
    public class IndexModel : PageModel
    {
        private readonly Dziennik.Data.SchoolContext _context;

        public IndexModel(Dziennik.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Teacher> Teacher { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Teacher != null)
            {
                Teacher = await _context.Teacher.ToListAsync();
            }
        }
    }
}
