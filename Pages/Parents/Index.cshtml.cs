using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dziennik.Data;
using Dziennik.Models;

namespace Dziennik.Pages.Parents
{
    public class IndexModel : PageModel
    {
        private readonly Dziennik.Data.SchoolContext _context;

        public IndexModel(Dziennik.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Parent> Parent { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Parent != null)
            {
                Parent = await _context.Parent.ToListAsync();
            }
        }
    }
}
