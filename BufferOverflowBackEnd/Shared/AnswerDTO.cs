using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class AnswerDTO
    {
        public int AnswerID { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public UserDTO Author { get; set; }

        public int QuestionID { get; set; }

        public List<VotingDTO> Votings { get; set; }
    }
}
