using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL.Models
{
    public class QuestionAnswerModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AnswerContent { get; set; }
        public bool IsCorrect { get; set; }

        public QuestionModel Question { get; set; }
    }
}
