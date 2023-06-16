using System;

namespace BookShelf.Domain.Models
{
    public sealed class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int Quantity { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
