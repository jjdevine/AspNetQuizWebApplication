using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QuizWebApplication.Services;

namespace QuizWebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JDService JDService;
        public DatabaseService DatabaseService;
        public string GeneratedValue { get; private set; }

        public string DBValue { get; set; }

        public int RandomNumber { get; private set; }

        public IndexModel(
            ILogger<IndexModel> logger, 
            JDService jDService,
            DatabaseService dbService)
        {
            _logger = logger;
            JDService = jDService;
            DatabaseService = dbService;
        }

        public void OnGet()
        {
            GeneratedValue = JDService.getJDServiceValue();

            DBValue = DatabaseService.GetDbVal();

            Random random = new Random();
            RandomNumber = random.Next(11);
        }
    }
}
