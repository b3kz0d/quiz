using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL.Models
{
    public class QuizSessionModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int QuizOptionId { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }

        public UserModel User { get; set; }
        public CategoryModel Category { get; set; }
        public QuizOptionModel QuizOption { get; set; }
    }
}
