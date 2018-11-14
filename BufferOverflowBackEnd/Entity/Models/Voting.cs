using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class Voting
    {
        public bool Status { get; set; }

        [Key, Column(Order = 0)]
        public int AnswerID { get; set; }
        public virtual Answer Answer { get; set; }

        [Key, Column(Order = 1)]
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
