using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApplication.Services
{
    public class DatabaseUtils
    {
        public static SqlConnection GetSQLConnection()
        {
            return new SqlConnection(GetConnectionString().ConnectionString);
        }

        private static SqlConnectionStringBuilder GetConnectionString()
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
