using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebApplication.Extensions;
using QuizWebApplication.Models;
using QuizWebApplication.Services;

namespace QuizWebApplication.Pages.QuizPages
{
    [Authorize]
    public class QuizEditorModel : PageModel
    {
        private readonly IQuizRepository QuizRepository;
        public QuizEditorModel(IQuizRepository quizRepository)
        {
            this.QuizRepository = quizRepository;
        }

        //routing param
        [StringLength(50, MinimumLength = 3)]
        [Required]
        [BindProperty]
        public string QuizId { get; set; }

        [BindProperty]
        public bool IsNewQuiz { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        [BindProperty]
        public string QuizName { get; set; }

        [MinLength(3)]
        [Required]
        [BindProperty]
        public string QuizText { get; set; }

        private readonly List<String> errorList = new List<string>();
        public List<String> ErrorList { get { return errorList; } }

        private readonly List<String> infoList = new List<string>();
        public List<String> InfoList { get { return infoList; } }

        public String Username { get; set; }

        public String UserId { get; set; }

        public void OnGet(string quizId)
        {
            QuizId = quizId;
            IsNewQuiz = quizId.Equals("new");

            if(IsNewQuiz)
            {
                QuizText = "Sample Question 1=Sample Answer 1\nSample Question 2=Sample Answer 2\nSample Question 3=Sample Answer 3";
            }
            else
            {
                Guid QuizIdAsGuid = Guid.Parse(QuizId);
                QuizName = QuizRepository.LoadQuizById(QuizIdAsGuid).QuizName;
                QuizText = LoadQuizText(QuizIdAsGuid);
            }

            Username = UserUtils.GetUserFriendlyName(User);
        }

        public IActionResult OnPost()
        {
            UserId = UserUtils.GetUserSubject(User);

            if (!ModelState.IsValid)
            {
                errorList.Add("Quiz is Invalid");
                return Page();
            }

            Quiz quiz = ParseQuiz();
            List<QuizQuestion> quizQuestions = ParseQuizQuestions(quiz.Id);

            if (quizQuestions.Count == 0)
            {
                errorList.Add("Quiz must have at least one question!");
                return Page();
            }

            if (quizQuestions.Count > 1000)
            {
                errorList.Add($"1000 Questions is the maximum - you have {quizQuestions.Count}");
                return Page();
            }

            if (IsNewQuiz)
            {
                List<Quiz> existingQuizzes = QuizRepository.LoadQuizzesForUser(UserId);
                IEnumerable<Quiz> quizzesWithSameName = from existingQuiz in existingQuizzes
                                                        where existingQuiz.QuizName.ToLower().Equals(quiz.QuizName.ToLower())
                                                        select quiz;
                Console.WriteLine($"quizzesWithSameName = {quizzesWithSameName.Count()}");
                if (quizzesWithSameName.Count() > 0)
                {
                    errorList.Add($"You already have a quiz called '{quiz.QuizName}'");
                    return Page();
                }
            }

            bool success = QuizRepository.PersistQuiz(quiz);
            Console.WriteLine(success ? "Quiz Persisted Successfully" : "Quiz failed to persist");

            success = QuizRepository.PersistQuizQuestions(quizQuestions);
            Console.WriteLine(success ? "Quiz Questions Persisted Successfully" : "Quiz Questions failed to persist");

            if(success)
            {
                infoList.Add($"Quiz [{QuizName}] saved successfully!");
            } 
            else
            {
                errorList.Add($"Quiz [{QuizName}] failed to save");
            }

            return Page();
        }

        private Quiz ParseQuiz()
        {
            Quiz quiz = new Quiz();

            if (QuizId.Equals("new"))
            {
                Console.WriteLine("new quiz");
                quiz.Id = Guid.NewGuid();
            }
            else
            {
                Console.WriteLine("Updating existing quiz");
                quiz.Id = Guid.Parse(QuizId); 
            }

            quiz.QuizName = QuizName;
            quiz.Username = UserUtils.GetUserSubject(User);

            Console.WriteLine($"Parsed Quiz: [{quiz}]");

            return quiz;
        }

        private List<QuizQuestion> ParseQuizQuestions(Guid quizId)
        {
            List<QuizQuestion> quizQuestions = new List<QuizQuestion>();

            string[] quizLines = QuizText.Split('\n');

            int index = 1;
            foreach (string quizLine in quizLines)
            {
                if (quizLine.Contains('=') && quizLine[0] != '#')
                {
                    string[] tokens = quizLine.Split('=');
                    if (tokens.Length == 2
                        && tokens[0].Length > 0
                        && tokens[1].Length > 0)
                    {
                        quizQuestions.Add(new QuizQuestion(
                            Guid.NewGuid(),
                            quizId,
                            tokens[0].Trim(),
                            tokens[1].Trim(),
                            index++));
                    }
                }
            }

            return quizQuestions;
        }

        private string LoadQuizText(Guid quizId)
        {
            List<QuizQuestion> quizQuestions = QuizRepository.LoadQuizQuestions(quizId, -1, false);

            StringBuilder result = new StringBuilder();

            quizQuestions.ForEach(question => result.Append($"{question.Question}={question.Answer}\n"));

            return result.ToString();
        }


    }
}
