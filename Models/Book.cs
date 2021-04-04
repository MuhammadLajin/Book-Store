using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Day4BookStore.Models
{
    public class Book
    {
        [Key]
        public int book_id { get; set; }

        [StringLength(250)]
        public string book_title { get; set; }
        public string book_bref { get; set; }
        public string book_desc { get; set; }
        public string book_pdf { get; set; }


        //relation with the book
        [ForeignKey("book_user")]
        public int? user_id { get; set; }     
        //make virtual to make laazy loading 
        public virtual User book_user { get; set; }


        [ForeignKey("book_Catalog")]
        public int cat_id { get; set; }
        public virtual Catalog book_Catalog { get; set; }


        [ForeignKey("book_Author")]
        public int auth_id { get; set; }
        public virtual Author book_Author { get; set; }
    }
}