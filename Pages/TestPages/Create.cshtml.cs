using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuizWebApplication.Data;
using QuizWebApplication.Models;

namespace QuizWebApplication.Pages.TestPages
{
    public class CreateModel : PageModel
    {
        private readonly QuizWebApplication.Data.QuizWebApplicationContext _context;

        public CreateModel(QuizWebApplication.Data.QuizWebApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DataModel1 DataModel1 { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DataModel1.Add(DataModel1);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
