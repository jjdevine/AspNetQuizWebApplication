using Microsoft.Extensions.Configuration;
using QuizWebApplication.EntityFramework;
using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWebApplication.Services
{
    public class QuizRepository : IQuizRepository
    {
        private readonly IConfiguration Configuration;

        private readonly QuizContext QuizContext;

        public QuizRepository(IConfiguration configuration, QuizContext quizContext)
        {
            Configuration = configuration;
            this.QuizContext = quizContext;
        }

        public bool PersistQuiz(Quiz quiz)
        {
            Console.WriteLine(quiz);

            //delete the quiz if it already exists so we can recreate it
            DeleteQuiz(quiz.Id);
            //QuizContext.Quizzes.Where(q => q.Id.Equals(quiz.Id)).DeleteFromQuery();

            QuizContext.Quizzes.Add(quiz);
            QuizContext.SaveChanges();

            return true;
        }

        public bool DeleteQuiz(Guid quizId)
        {
            QuizContext.QuizQuestions.Where(q => q.QuizId.Equals(quizId)).DeleteFromQuery();
            QuizContext.Quizzes.Where(q => q.Id.Equals(quizId)).DeleteFromQuery();
            QuizContext.SaveChanges();
            return true;
        }

        public bool PersistQuizQuestions(List<QuizQuestion> quizQuestions)
        {
            QuizContext.QuizQuestions.AddRange(quizQuestions);
            QuizContext.SaveChanges();
            return true;
        }

        public List<Quiz> LoadQuizzesForUser(string username)
        {
            return QuizContext.Quizzes.Where(q => q.Username.ToLower().Equals(username.ToLower())).ToList();
        }

        public Quiz LoadQuizById(Guid quizId)
        {
            String sql = "SELECT QuizId, [User], QuizName FROM [quiz].[UserQuizzes] WHERE [QuizId] = @QuizId";

            using SqlConnection connection = DatabaseUtils.GetSQLConnection(Configuration);
            using SqlCommand command = new SqlCommand(sql.ToString(), connection);

            command.Parameters.Add($"@QuizId", System.Data.SqlDbType.UniqueIdentifier).Value = quizId;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                return QuizRowMapper.MapRow(reader);
            }

            throw new ArgumentException($"Quiz with ID [{quizId}] was not found in the Database");
        }

        public List<QuizQuestion> LoadQuizQuestions(Guid quizId, int listSize, bool randomOrder)
        {
            String topClause = "";
            String orderClause = "[Order]";

            if (listSize > 0)
            {
                topClause = $"TOP {listSize}";
                
            } 
            if(randomOrder)
            {
                orderClause = "NEWID()";
            }

            String sql = $"SELECT {topClause} QuestionId, QuizId, Question, Answer, [Order] " +
                "FROM [quiz].[QuizQuestions] " +
                "WHERE QuizId = @QuizId " +
                $"ORDER BY {orderClause}";

            using SqlConnection connection = DatabaseUtils.GetSQLConnection(Configuration);
            using SqlCommand command = new SqlCommand(sql.ToString(), connection);

            command.Parameters.Add($"@QuizId", System.Data.SqlDbType.UniqueIdentifier).Value = quizId;

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            List<QuizQuestion> resultList = new List<QuizQuestion>();

            while (reader.Read())
            {
                resultList.Add(new QuizQuestion(
                        reader.GetGuid(0),
                        reader.GetGuid(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetInt32(4)
                    ));
            }

            if(!randomOrder)
            {
                resultList.Sort((a, b) => a.Order.CompareTo(b.Order));
            }

            return resultList;
        }

        private class QuizRowMapper {
            public static Quiz MapRow(SqlDataReader reader)
            {
                return new Quiz()
                {
                    Id = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    QuizName = reader.GetString(2)
                };
            }
        }
    }


}
