using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BufferOverflow.Models
{
    public class LoginUser
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}