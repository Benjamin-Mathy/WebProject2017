using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebProject2017.Models
{
    public class SignInModelView
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le login est invalide")]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le mot de passe est invalide")]
        public string Password { get; set; }
    }
}