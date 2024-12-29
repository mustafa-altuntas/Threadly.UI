using Threadly.UI.Models.ViewModels.Comment;
using Threadly.UI.Models.ViewModels.Community;
using Threadly.UI.Models.ViewModels.Users;

namespace Threadly.UI.Models.ViewModels.Post
{
    public class PostVM
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string CommunityId { get; set; }

        public int UpVotes { get; set; } 
        public int DownVotes { get; set; } 

        // Navigation Property
        public UserVM? Author { get; set; }
        public CommunityVM? Community { get; set; }
        public ICollection<CommentListVM>? Comments { get; set; }



    }
}
