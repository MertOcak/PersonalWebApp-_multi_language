using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PersonalWebApp.Models
{
    public class ContactRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please check your email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please check your subject")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required(ErrorMessage ="Please check your name")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please check your message")]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
