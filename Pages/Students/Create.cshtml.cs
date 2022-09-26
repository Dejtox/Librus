using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dziennik.Data;
using Dziennik.Models;

namespace Dziennik.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly Dziennik.Data.SchoolContext _context;

        public CreateModel(Dziennik.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClassID"] = new SelectList(_context.Class, "ClassID", "ClassID");
        ViewData["ParentID"] = new SelectList(_context.Parent, "ParentID", "ParentID");
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Student.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
