using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dziennik.Data;
using Dziennik.Models;

namespace Dziennik.Pages.Classes
{
    public class DeleteModel : PageModel
    {
        private readonly Dziennik.Data.SchoolContext _context;

        public DeleteModel(Dziennik.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Class Class { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }

            var classe = await _context.Class.FirstOrDefaultAsync(m => m.ClassID == id);

            if (classe == null)
            {
                return NotFound();
            }
            else 
            {
                Class = classe;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }
            var classe = await _context.Class.FindAsync(id);

            if (classe != null)
            {
                Class = classe;
                _context.Class.Remove(Class);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
