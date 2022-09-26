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
    public class DeleteModel : PageModel
    {
        private readonly Dziennik.Data.SchoolContext _context;

        public DeleteModel(Dziennik.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Parent Parent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Parent == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent.FirstOrDefaultAsync(m => m.ParentID == id);

            if (parent == null)
            {
                return NotFound();
            }
            else 
            {
                Parent = parent;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Parent == null)
            {
                return NotFound();
            }
            var parent = await _context.Parent.FindAsync(id);

            if (parent != null)
            {
                Parent = parent;
                _context.Parent.Remove(Parent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
