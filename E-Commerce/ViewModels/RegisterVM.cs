using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Please Enter Your Name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please Enter Your Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please Enter Your Address")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Please Enter Your Password")]
        [Compare("ConfirmPassword",ErrorMessage ="Password and confirm Password must match")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password")]
        [Compare("Password",ErrorMessage = "Password and confirm Password must match")]

        public string ConfirmPassword { get; set; }
    }
}
