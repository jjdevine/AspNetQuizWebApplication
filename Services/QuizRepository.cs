using Microsoft.Extensions.Configuration;
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

        public QuizRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool PersistQuiz(Quiz quiz)
        {
            Console.WriteLine($"Persisting Quiz - {quiz}");

            String sql = "INSERT INTO [quiz].[UserQuizzes] (QuizId, [User], QuizName) values (@QuizId, @Username, @QuizName);";

            using SqlConnection connection = DatabaseUtils.GetSQLConnection(Configuration);
            using SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.Add("@QuizId", System.Data.SqlDbType.UniqueIdentifier).Value = quiz.Id;
            command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar, 50).Value = quiz.Username;
            command.Parameters.Add("@QuizName", System.Data.SqlDbType.NVarChar, 50).Value = quiz.QuizName;

            connection.Open();
            int result = command.ExecuteNonQuery();
            return result > 0;
        }

        public bool PersistQuizQuestions(List<QuizQuestion> quizQuestions)
        {
            if(quizQuestions.Count == 0)
            {
                Console.WriteLine("No questions provided!");
                return false;
            }

            // batch inserting - http://richorama.github.io/2017/11/28/sql-bulk-copy/

            StringBuilder sql = new StringBuilder("INSERT INTO [quiz].[QuizQuestions] ([QuestionId], [QuizId], [Question], [Answer], [Order]) VALUES \r\n" +
                "(@QuestionId0, @QuizId0, @Question0, @Answer0, @Order0)");

            for (var x = 1; x < quizQuestions.Count; x++)
            {
                sql.Append($",\r\n (@QuestionId{x}, @QuizId{x}, @Question{x}, @Answer{x}, @Order{x})");
            }

            using SqlConnection connection = DatabaseUtils.GetSQLConnection(Configuration);
            using SqlCommand command = new SqlCommand(sql.ToString(), connection);

            for (var x = 0; x < quizQuestions.Count; x++)
            {
                var thisQuizQuestion = quizQuestions[x];

                command.Parameters.Add($"@QuestionId{x}", System.Data.SqlDbType.UniqueIdentifier).Value = thisQuizQuestion.QuestionId;
                command.Parameters.Add($"@QuizId{x}", System.Data.SqlDbType.UniqueIdentifier).Value = thisQuizQuestion.QuizId;
                command.Parameters.Add($"@Question{x}", System.Data.SqlDbType.NVarChar, 4000).Value = thisQuizQuestion.Question;
                command.Parameters.Add($"@Answer{x}", System.Data.SqlDbType.NVarChar, 4000).Value = thisQuizQuestion.Answer;
                command.Parameters.Add($"@Order{x}", System.Data.SqlDbType.Int).Value = thisQuizQuestion.Order;
            }

            connection.Open();
            int result = command.ExecuteNonQuery();
            return result == quizQuestions.Count;


            /*
             * INSERT INTO [quiz].[QuizQuestions] ([QuestionId], [QuizId], [Question], [Answer]) VALUES
	        (newid(), newid(), 'test question', 'test answer'),
	        (newid(), newid(), 'test question2', 'test answer2')
             */
        }

        public List<Quiz> LoadQuizzesForUser(string username)
        {
            String sql = "SELECT QuizId, User, QuizName FROM [quiz].[UserQuizzes] WHERE UPPER([User]) = @Username";

            using SqlConnection connection = DatabaseUtils.GetSQLConnection(Configuration);
            using SqlCommand command = new SqlCommand(sql.ToString(), connection);

            command.Parameters.Add($"@Username", System.Data.SqlDbType.NVarChar, 50).Value = username.ToUpper();

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            List<Quiz> resultList = new List<Quiz>();

            while(reader.Read())
            {
                resultList.Add(new Quiz()
                {
                    Id = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    QuizName = reader.GetString(2)
                }); 
            }

            return resultList;
        }

        public List<QuizQuestion> LoadQuizQuestions(Guid quizId, int listSize)
        {
            String sql = "SELECT TOP "+listSize+" QuestionId, QuizId, Question, Answer, [Order] " +
                "FROM [quiz].[QuizQuestions] " +
                "WHERE QuizId = @QuizId " +
                "ORDER BY NEWID()";

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

            return resultList;
        }
    }


}
