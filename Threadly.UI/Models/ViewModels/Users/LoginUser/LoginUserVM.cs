using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Threadly.UI.Models.ViewModels.Users.LoginUser
{
    public class LoginUserVM
    {

        [Display(Name = "Email veya Kullanıcı Adı *", Prompt = "Email veya Kullanıcı Adı")]
        public string EmailOrUserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Parola *", Prompt = "Parola")]
        public string Password { get; set; }


    }
}
