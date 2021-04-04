using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Day4BookStore.Models
{
    public class Author
    {
        [Key]
        public int auth_id { get; set; }
        public string auth_name { get; set; }
        
        public virtual List<Book> auth_books { get; set; }
    }
}