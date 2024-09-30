using MVC_1_Depi.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_1_Depi.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required(ErrorMessage ="Email is Required")]
        [RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z]+.[a-zA-Z]{2,4}", ErrorMessage = "Invalid Email Format.")]
        [Display(Name ="Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(maximumLength:12,MinimumLength =10)]
        [Display(Name ="Phone Number")]
        public string Phone{ get; set; }

        [Required (ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength:30,MinimumLength =6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Confirmation is required")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 30, MinimumLength = 6)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
