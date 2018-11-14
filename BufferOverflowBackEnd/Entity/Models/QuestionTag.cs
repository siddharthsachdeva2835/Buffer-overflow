using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class QuestionTag
    {
        [Key, Column(Order = 0)]
        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }

        [Key, Column(Order = 1)]
        public int TagID { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
