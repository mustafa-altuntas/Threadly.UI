using System.ComponentModel.DataAnnotations;

namespace Threadly.UI.Models.ViewModels.Community
{
    public class CommunityCreateVM
    {
        [Display(Name = "Topluluk Adı *", Prompt = "Topluluk Adı")]
        public string? Name { get; set; }
        
        [Display(Name = "Açıklama *", Prompt = "Açıklama")]
        public string? Description { get; set; }
        
        ////[Display(Name = "Topluluk Adı *", Prompt = "aaaaaaa")]
        //public string? ImageUrl { get; set; }
        
        ////[Display(Name = "Topluluk Adı *", Prompt = "aaaaaaa")]
        //public string? OwnerId { get; set; }

        public IFormFileCollection? File { get; set; }

    }
}
