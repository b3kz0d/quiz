using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL.Models
{
    public class AnswerResultModel
    {
        public int Id { get; set; }
        public int ResultId { get; set; }
        public int QuestionAnswerId { get; set; }

        public ResultModel Result { get; set; }
        public QuestionAnswerModel QuestionAnswer { get; set; }
    }
}
