using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWebApplication.Services
{
    public class DatabaseService
    {

        // see https://docs.microsoft.com/en-us/azure/azure-sql/database/connect-query-dotnet-core

        public DatabaseService(IWebHostEnvironment webHostEnvironment)
        {
            this.WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public String GetDbVal()
        {
            return "fake";
            /*
            StringBuilder result = new StringBuilder();

            using (SqlConnection connection = GetSQLConnection())
            {
                connection.Open();

                String sql = "SELECT col1 FROM dbo.quiztesttable";

                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                {
                    {
                        while (reader.Read())
                        {
                            result.AppendLine($"db value {reader.GetString(0)}");
                        }
                    }
                }
            }

            return result.Length > 0 ? result.ToString() : "No Values Found!";
            */
        }

        public SqlConnection GetSQLConnection()
        {
            return new SqlConnection(GetConnectionString().ConnectionString);

        }

        private SqlConnectionStringBuilder GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "quizwebappdb.database.windows.net";
            builder.UserID = "jjdevine";
            builder.Password = "QuizWebApp1";
            builder.InitialCatalog = "QuizDB";

            return builder;
        }


    }
}
