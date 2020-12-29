using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApplication.Models
{
    public class QuizQuestion
    {
        public QuizQuestion(Guid questionId, Guid quizId, string question, string answer)
        {
            this.QuestionId = questionId;
            this.QuizId = quizId;
            this.Question = question;
            this.Answer = answer;
        }

        public Guid QuestionId { get; set; }
        public Guid QuizId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public override string ToString()
        {
            return $"Quiz Question id [{QuestionId}], QuizId [{QuizId}], Question [{Question}], Answer [{Answer}]";
        }
    }
}
