using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BufferOverflow.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User Author { get; set; }

        public int QuestionID { get; set; }
        public List<Voting> Votings { get; set; }
    }
}