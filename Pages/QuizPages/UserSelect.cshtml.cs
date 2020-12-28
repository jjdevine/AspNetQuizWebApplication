using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebApplication.Extensions;

namespace QuizWebApplication.Pages.QuizPages
{
    public class UserSelectModel : PageModel
    {

        [StringLength(50, MinimumLength = 3)]
        [Required]
        [BindProperty]
        public string Username { get; set; }

        private readonly List<String> errorList = new List<string>();
        public List<String> ErrorList { get { return errorList; } }



        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorList.Add("Username must be provided, minimum length 3, maximum length 50");
                return Page();
            }
            else
            {
                var SessionState = SessionUtils.GetSessionState(HttpContext.Session);
                SessionState.Username = Username;
                SessionUtils.UpdateSessionState(HttpContext.Session, SessionState);
                return RedirectToPage("/QuizPages/QuizSelect");
            }
        }
    }
}
