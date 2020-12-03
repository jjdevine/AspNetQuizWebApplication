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
    public class IndexModel : PageModel
    {
        private readonly QuizWebApplication.Data.QuizWebApplicationContext _context;

        public IndexModel(QuizWebApplication.Data.QuizWebApplicationContext context)
        {
            _context = context;
        }

        public IList<DataModel1> DataModel1 { get;set; }

        public async Task OnGetAsync()
        {
            DataModel1 = await _context.DataModel1.ToListAsync();
        }
    }
}
