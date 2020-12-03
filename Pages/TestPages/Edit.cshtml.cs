using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizWebApplication.Data;
using QuizWebApplication.Models;

namespace QuizWebApplication.Pages.TestPages
{
    public class EditModel : PageModel
    {
        private readonly QuizWebApplication.Data.QuizWebApplicationContext _context;

        public EditModel(QuizWebApplication.Data.QuizWebApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DataModel1 DataModel1 { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DataModel1 = await _context.DataModel1.FirstOrDefaultAsync(m => m.ID == id);

            if (DataModel1 == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DataModel1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataModel1Exists(DataModel1.ID))
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

        private bool DataModel1Exists(int id)
        {
            return _context.DataModel1.Any(e => e.ID == id);
        }
    }
}
