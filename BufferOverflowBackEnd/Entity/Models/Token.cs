using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Token
    {
        [ForeignKey("User")]
        public int TokenID { get; set; }
        public int TokenString { get; set; }

        public virtual User User { get; set; }
    }
}
