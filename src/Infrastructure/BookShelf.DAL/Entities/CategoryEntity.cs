using System.Collections.Generic;

namespace BookShelf.DAL.Entities
{
    public sealed class CategoryEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<BookEntity> Books { get; set; } = new List<BookEntity>();
    }
}
