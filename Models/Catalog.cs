using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Day4BookStore.Models
{
    public class Catalog
    {
        [Key]
        public int cat_id { get; set; }
        public string cat_name { get; set; }
        public string cat_desc { get; set; }
        public virtual List<Book> cat_books { get; set; }
    }
}