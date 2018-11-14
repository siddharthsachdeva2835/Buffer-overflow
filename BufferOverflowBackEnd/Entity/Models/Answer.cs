using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User Author { get; set; }

        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }

        public virtual ICollection<Voting> Votings { get; set; }

    }
}
