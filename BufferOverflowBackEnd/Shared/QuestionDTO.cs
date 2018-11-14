using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class QuestionDTO
    {
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AnswerCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserDTO Author { get; set; }

        public List<AnswerDTO> Answers { get; set; }
        public List<String> Tags { get; set; }
    }
}
