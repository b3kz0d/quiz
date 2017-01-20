using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL.Models
{
    public class ResultModel
    {
        public int Id { get; set; }
        public int QuizSessionId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionOrder { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public QuizSessionModel QuizSession { get; set; }
        public QuestionModel Question { get; set; }
    }
}
