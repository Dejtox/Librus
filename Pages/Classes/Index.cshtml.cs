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

namespace Dziennik.Pages.Classes
{    
    public class IndexModel : PageModel
    {
        private readonly Dziennik.Data.SchoolContext _context;

        public IndexModel(Dziennik.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Class> Class { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Class != null)
            {
                Class = await _context.Class
                .Include(t => t.Teacher).ToListAsync();
            }
        }
    }
}
