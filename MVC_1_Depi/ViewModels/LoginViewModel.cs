using System.ComponentModel.DataAnnotations;

namespace MVC_1_Depi.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required (ErrorMessage ="Email is required")]
        [RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z]+.[a-zA-Z]{2,4}", ErrorMessage = "Invalid Email Format.")]
        public string Email{ get; set; }

        [DataType(DataType.Password)] 
        [Required (ErrorMessage ="Password is required")]
        public string Password{ get; set; }
    }
}
