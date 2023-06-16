namespace BookShelf.Domain.Models
{
    public sealed class UpdateBookModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int? PublicationYear { get; set; }
        public int? Quantity { get; set; }
    }
}
