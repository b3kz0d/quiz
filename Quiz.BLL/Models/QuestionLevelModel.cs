using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL.Models
{
    public class QuestionLevelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
    }
}
