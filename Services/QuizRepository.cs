using QuizWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApplication.Services
{
    public class QuizRepository : IQuizRepository
    {
        public bool PersistQuiz(Quiz quiz)
        {
            Console.WriteLine($"Persisting Quiz - {quiz}");

            String sql = "INSERT INTO [quiz].[UserQuizzes] (QuizId, [User], QuizName) values (@QuizId, @Username, @QuizName);";

            using SqlConnection connection = DatabaseUtils.GetSQLConnection();
            using SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.Add("@QuizId", System.Data.SqlDbType.UniqueIdentifier).Value = quiz.Id;
            command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar, 50).Value = quiz.Username;
            command.Parameters.Add("@QuizName", System.Data.SqlDbType.NVarChar, 50).Value = quiz.QuizName;

            connection.Open();
            int result = command.ExecuteNonQuery();
            return result > 0;
        }
    }
}
