using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<QuestionTag> QuestionTags { get; set; }
    }
}
