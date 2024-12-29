using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Threadly.UI.Models.ViewModels.Users
{
    public class CreateUserVM
    {
        [Display(Name = "Kullanıcı Adı *", Prompt ="Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "Email *", Prompt = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Parola *", Prompt = "Parola")]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Parola Tekrar *", Prompt = "Parola Tekrar")]
        public string PasswordConfirm { get; set; }
    }
}
