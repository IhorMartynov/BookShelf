using System;

namespace BookShelf.DAL.Entities
{
    public sealed class BookEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int Quantity { get; set; }

        public long CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
}
