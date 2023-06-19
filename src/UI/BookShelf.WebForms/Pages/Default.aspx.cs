using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookShelf.Application.Contracts.Services;

namespace BookShelf.WebForms
{
    public partial class _Default : Page
    {
        private readonly IBooksService _bookService;

        public _Default(IBooksService bookService)
        {
            _bookService = bookService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var books = _bookService.GetAllBooksAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            BooksGridView.DataSource = books;
        }

        protected void BookDetailsLinkButton_Click(object sender, EventArgs e)
        {
            var bookId = GetBookId(sender as Button);

        }

        protected void BookDeleteLinkButton_Click(object sender, EventArgs e)
        {
            var bookId = GetBookId(sender as Button);
        }

        private static Guid GetBookId(Button button)
        {
            return Guid.TryParse(button.CommandArgument, out var id) ? id : Guid.Empty;
        }
    }
}