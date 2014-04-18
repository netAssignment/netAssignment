using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BookManagement.Core.Models
{
    public class Book
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public String Author { get; set; }

        public float Price { get; set; }

        public String Overview { get; set; }

        public String PathImage { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> ListComment { get; set; }
    }
}
