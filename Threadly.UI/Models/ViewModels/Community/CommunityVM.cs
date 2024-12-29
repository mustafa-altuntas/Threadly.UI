using P = Threadly.UI.Models.ViewModels.Post;
using Threadly.UI.Models.ViewModels.Users;

namespace Threadly.UI.Models.ViewModels.Community
{
    public class CommunityVM
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string OwnerId { get; set; }


        // Navigation Property
        //public ICollection<UserVM>? Moderators { get; set; }
            

        public UserVM Owner { get; set; }
        public ICollection<P.PostVM> Posts { get; set; }

    }
}
