using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Required")]
        [MinLength(5, ErrorMessage ="Username should be at least 5 symbols")]
        [Display(Name="Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required")]
        [MinLength(5, ErrorMessage = "Username should be at least 5 symbols")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        
    }
}
