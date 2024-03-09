using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Please Enter your name")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Please Enter Your Password")]
        public string Password { get; set; }
        public bool IsPresistent { get; set; }
    }
}
