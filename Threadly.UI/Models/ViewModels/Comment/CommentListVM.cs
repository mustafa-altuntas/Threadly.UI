namespace Threadly.UI.Models.ViewModels.Comment
{
    public class CommentListVM
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; } // Yorum içeriği
        public string AuthorName { get; set; } // Yazar kimliği
        public string AuthorId { get; set; } // Yazar kimliği
        public List<CommentListVM> Children { get; set; } // Alt yorumlar

    }
}
