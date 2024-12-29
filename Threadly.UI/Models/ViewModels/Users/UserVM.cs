using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Threadly.UI.Models.ViewModels.Users
{
    public class UserVM
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? NameSurname { get; set; }
        public string? ImageUrl { get; set; }

    }
}
