using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Areas.Admin.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please check your email address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please check your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        public string AccessDenied { get; set; }
    }
}
