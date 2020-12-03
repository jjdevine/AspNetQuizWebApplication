using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWebApplication.Services;

namespace QuizWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizWebApplicationController : ControllerBase
    {
        public QuizWebApplicationController(JDService jDService)
        {
            this.JDService = jDService;
        }

        public JDService JDService { get; }

        [HttpGet]
        public String Get()
        {
            return JDService.getJDServiceValue();
        }

        [Route("subservice")]
        [HttpGet]
        public String Get(
            [FromQuery(Name="theParam")] string Param1)
        {
            Console.WriteLine(Param1 ?? "No param provided");
            return "you found the subservice";
        }
    }
}
