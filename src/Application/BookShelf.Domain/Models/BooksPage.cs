using System.Collections.Generic;

namespace BookShelf.Domain.Models
{
    public sealed class BooksPage
    {
        public IEnumerable<Book> Books { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
