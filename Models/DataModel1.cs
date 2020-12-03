using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizWebApplication.Models
{
    public class DataModel1 
    {
        public int ID { get; set; }
        public string Data { get; set;  }

        [DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; }
    }
}
