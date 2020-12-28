using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuizWebApplication.Pages.QuizPages
{
    public class QuizEditorModel : PageModel
    {
        //routing param
        public string QuizId { get; set; }

        public bool IsNewQuiz { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required]
        [BindProperty]
        public string QuizName { get; set; }

        [BindProperty]
        public string QuizText { get; set; }

        private readonly List<String> errorList = new List<string>();
        public List<String> ErrorList { get { return errorList; } }

        public void OnGet(string quizId)
        {
            QuizId = quizId;
            IsNewQuiz = quizId.Equals("new");
        }

        public void OnPost()
        {
            Console.WriteLine($"QuizText is {QuizText}");
        }
    }
}
