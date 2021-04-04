using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Day4BookStore.Models
{
    public class User
    {
        //user id
        //[Display(Name = "user id")]
        [Key]
        public int user_id { get; set; }

        //user name
        [Required(ErrorMessage = "*")]
        [Display(Name = "Full Name")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "short name")]
        public string user_name { get; set; }

        //user age
        [Required(ErrorMessage = "*")]
        [Range(20, 60, ErrorMessage = "age must between 20 and 60")]
        public int? user_age { get; set; }

        //user email
        //[DataType(DataType.)]
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "invalid Email")]
        public string user_email { get; set; }

        //user password
        public string user_password { get; set; }
        [NotMapped]
        [Compare("user_password", ErrorMessage = "password not match")]
        public string confirm_password { get; set; }

        //user photo
        public string user_photo { get; set; }

        //user relation with book 1 to Many books 
        public virtual List<Book> user_books { get; set; }
        
    }
}