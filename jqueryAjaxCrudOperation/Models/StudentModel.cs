using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jqueryAjaxCrudOperation.Models
{
    public class StudentModel
    {
        [Key]
        public int StudentId { get; set; }
        [Column(TypeName = "varchar(50)")]
        [Required(ErrorMessage ="Please Enter your Name")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)")]
        [Required(ErrorMessage ="Please Enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Please Enter Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Column(TypeName="varchar(50)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(10)")]
        [Required(ErrorMessage ="Please Enter Password")]
        public string Password { get; set; }

        [Column(TypeName = "varchar(10)")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "please Not a valid mobile number")]
        [Required(ErrorMessage ="Please Enter your Mobile Number.")]
        public string MobileNo { get; set; }
    }
}
