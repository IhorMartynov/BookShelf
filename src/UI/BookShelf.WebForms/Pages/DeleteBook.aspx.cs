using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using BookShelf.Application.Contracts.Services;

namespace BookShelf.WebForms.Pages
{
    public partial class DeleteBook : Page
    {
        private readonly IBooksService _booksService;

        public DeleteBook(IBooksService booksService)
        {
            _booksService = booksService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var idParameter = Request.QueryString["id"];
            var id = Guid.TryParse(idParameter, out var value) ? value : Guid.Empty;

            if (!Page.IsPostBack)
            {
                Page.RegisterAsyncTask(new PageAsyncTask(ct => PopulateBookDetails(id, ct)));
            }
        }

        private async Task PopulateBookDetails(Guid id, CancellationToken cancellationToken)
        {
            var book = await _booksService.GetBookByIdAsync(id, cancellationToken);

            if (book is null) return;

            IdHiddenField.Value = book.Id.ToString();
            TitleLabel.Text = book.Title;
            AuthorLabel.Text = book.Author;
            ISBNLabel.Text = book.ISBN;
            PublicationYearLabel.Text = book.PublicationYear.ToString();
            QuantityLabel.Text = book.Quantity.ToString();
        }

        protected void DeleteButton_OnClick(object sender, EventArgs e) =>
            Page.RegisterAsyncTask(new PageAsyncTask(DeleteCurrentBook));

        private async Task DeleteCurrentBook(CancellationToken cancellationToken)
        {
            var id = Guid.TryParse(IdHiddenField.Value, out var value) ? value : Guid.Empty;

            await _booksService.DeleteBookAsync(id, cancellationToken);

            Response.Redirect("Default.aspx");
        }
    }
}