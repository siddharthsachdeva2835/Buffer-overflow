using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity.Models;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageURL { get; set; }

        public virtual Token Token { get; set; }

        public virtual ICollection<Voting> Votings { get; set; }
    }
}
