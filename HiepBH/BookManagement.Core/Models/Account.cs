using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BookManagement.Core.Models
{
    public class Account
    {
        [Key]
        public String UserName { get; set; }

        public String Password { get; set; }
    }
}
