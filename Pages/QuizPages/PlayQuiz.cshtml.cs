using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebApplication.Extensions;
using QuizWebApplication.Models;
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

        public List<QuizQuestion> QuizQuestions { get; set; }

        public Quiz Quiz { get; set; }

        public string Username { get; set; }

        public void OnGet(string quizId)
        {
            Quiz = QuizRepository.LoadQuizById(Guid.Parse(quizId));
            Console.WriteLine($"Playing Quiz - {Quiz.QuizName}");

            //50 questions max
            QuizQuestions = QuizRepository.LoadQuizQuestions(Guid.Parse(quizId), 50, true);

            Username = SessionUtils.GetSessionState(HttpContext.Session)?.Username;
        }
    }
}
