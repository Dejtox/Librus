using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dziennik.Data;
using Dziennik.Models;

namespace Dziennik.Pages.Classes
{
    public class EditModel : PageModel
    {
        private readonly Dziennik.Data.SchoolContext _context;

        public EditModel(Dziennik.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Class Class { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }

            var classe =  await _context.Class.FirstOrDefaultAsync(m => m.ClassID == id);
            if (classe == null)
            {
                return NotFound();
            }
            Class = classe;
           ViewData["TeacherID"] = new SelectList(_context.Teacher, "TeacherID", "TeacherID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Class).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(Class.ClassID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClassExists(int id)
        {
          return _context.Class.Any(e => e.ClassID == id);
        }
    }
}
