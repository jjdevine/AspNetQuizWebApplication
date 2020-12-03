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
        public string GeneratedValue { get; private set; }


        public IndexModel(
            ILogger<IndexModel> logger, 
            JDService jDService)
        {
            _logger = logger;
            JDService = jDService;
        }

        public void OnGet()
        {
            GeneratedValue = JDService.getJDServiceValue();
        }
    }
}
