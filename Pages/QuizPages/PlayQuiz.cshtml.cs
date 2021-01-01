using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebApplication.Services;

namespace QuizWebApplication.Pages.QuizPages
{
    public class PlayQuizModel : PageModel
    {
        private readonly IQuizRepository QuizRepository;
        public PlayQuizModel(IQuizRepository quizRepository)
        {
            this.QuizRepository = quizRepository;
        }

        //routing param
        public string QuizId { get; set; }

        public void OnGet(string quizId)
        {
            Console.WriteLine($"Playing Quiz - {quizId}");

            QuizRepository.LoadQuizQuestions(Guid.Parse(quizId), 50).ForEach(question => Console.WriteLine($"Question [{question.Question}], Answer [{question.Answer}]"));

            QuizId = quizId;
        }
    }
}
