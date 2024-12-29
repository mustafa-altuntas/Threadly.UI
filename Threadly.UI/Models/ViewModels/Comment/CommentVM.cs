using Threadly.UI.Models.ViewModels.Users;

namespace Threadly.UI.Models.ViewModels.Comment
{
    public class CommentVM
    {
        public string Content { get; set; }
        public string ParentId { get; set; }
        public string AuthorId { get; set; }
 

        // Navigation Property
        public UserVM? Author { get; set; }
 
        public List<CommentVM> Children { get; set; }
    }
}
