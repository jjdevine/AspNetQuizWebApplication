using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApplication.Services
{
    public class JDService
    {
        public JDService(IWebHostEnvironment webHostEnvironment)
        {
            this.WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public string getJDServiceValue()
        {
            Random random = new Random();

            return random.Next() + "---" + random.Next();
        }
    }
}
