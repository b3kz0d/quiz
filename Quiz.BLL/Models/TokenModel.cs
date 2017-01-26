using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.BLL.Models
{
    public class TokenModel
    {
        public string AuthToken { get; set; }
        public int UserId { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
