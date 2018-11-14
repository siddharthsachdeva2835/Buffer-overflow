using System;
using System.Collections.Generic;
using System.Linq;
using Entity.Models;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AnswerCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User Author { get; set; }

        public virtual ICollection<QuestionTag> QuestionTags { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
