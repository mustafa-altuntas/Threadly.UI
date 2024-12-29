namespace Threadly.UI.Models.ViewModels.Comment
{
    public class CommentCreateVM
    {
        public string Content { get; set; } // Yorum içeriği
        public string ParentId { get; set; } // Üst yorum kimliği
        public string AuthorId { get; set; } // Yazar kimliği
        public string PostId { get; set; } // Gönderi kimliği
    }
}
