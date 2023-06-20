using System;
using System.Threading;
using System.Threading.Tasks;
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

        protected async void Page_Load(object sender, EventArgs e)
        {
            Page.RegisterAsyncTask(new PageAsyncTask(BindBooksGridViewDataSource));
        }

        private async Task BindBooksGridViewDataSource(CancellationToken cancellationToken)
        {
            var books = await _bookService.GetAllBooksAsync(cancellationToken);
            BooksGridView.DataSource = books;
            BooksGridView.DataBind();
        }
    }
}