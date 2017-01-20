using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int QuestionLevelId { get; set; }
        public string QuestionContent { get; set; }

        public CategoryModel Category { get; set; }
        public QuestionLevelModel QuestionLevel { get; set; }
    }
}
