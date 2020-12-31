using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApplication.Services
{
    public class DatabaseUtils
    {
        public static SqlConnection GetSQLConnection(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:QuizWebApplicationContext"];
            Console.WriteLine($"connectionString is - {connectionString}");

            return new SqlConnection(connectionString);
            //return new SqlConnection(GetConnectionString(configuration).ConnectionString);
        }

        private static SqlConnectionStringBuilder GetConnectionString(IConfiguration configuration)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "quizwebappdb.database.windows.net",
                UserID = "jjdevine",
                Password = "QuizWebApp1",
                InitialCatalog = "QuizDB"
            };

            return builder;
        }
    }


}
