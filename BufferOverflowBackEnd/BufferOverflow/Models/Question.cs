using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BufferOverflow.Models
{
    public class Question
    {
        public int QuestionID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int AnswerCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User Author { get; set; }

        public List<Answer> Answers { get; set; }
        public List<String> Tags { get; set; }
    }
}