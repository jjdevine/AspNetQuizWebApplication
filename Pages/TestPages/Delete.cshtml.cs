using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuizWebApplication.Data;
using QuizWebApplication.Models;

namespace QuizWebApplication.Pages.TestPages
{
    public class DeleteModel : PageModel
    {
        private readonly QuizWebApplication.Data.QuizWebApplicationContext _context;

        public DeleteModel(QuizWebApplication.Data.QuizWebApplicationContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DataModel1 = await _context.DataModel1.FindAsync(id);

            if (DataModel1 != null)
            {
                _context.DataModel1.Remove(DataModel1);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
