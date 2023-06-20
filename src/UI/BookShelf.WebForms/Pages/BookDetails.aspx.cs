using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using BookShelf.Application.Contracts.Services;
using BookShelf.Domain.Models;

namespace BookShelf.WebForms.Pages
{
    public partial class BookDetails : Page
    {
        private readonly IBooksService _booksService;

        public BookDetails(IBooksService booksService)
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
        protected void UpdateBookOnClick(object sender, EventArgs e) =>
            Page.RegisterAsyncTask(new PageAsyncTask(UpdateBookDetails));

        private async Task PopulateBookDetails(Guid id, CancellationToken cancellationToken)
        {
            var book = await _booksService.GetBookByIdAsync(id, cancellationToken);

            if (book is null) return;

            IdHiddenField.Value = book.Id.ToString();
            BookTitleLabel.Text = book.Title;
            TitleTextBox.Text = book.Title;
            AuthorTextBox.Text = book.Author;
            ISBNTextBox.Text = book.ISBN;
            PublicationYearTextBox.Text = book.PublicationYear.ToString();
            QuantityTextBox.Text = book.Quantity.ToString();
        }

        private async Task UpdateBookDetails(CancellationToken cancellationToken)
        {
            var id = Guid.TryParse(IdHiddenField.Value, out var value) ? value : Guid.Empty;

            var updateBookModel = new UpdateBookModel
            {
                Title = BookTitleLabel.Text,
                Author = AuthorTextBox.Text,
                ISBN = ISBNTextBox.Text,
                PublicationYear = int.TryParse(PublicationYearTextBox.Text, out var year) ? year : (int?)null,
                Quantity = int.TryParse(QuantityTextBox.Text, out var quantity) ? quantity : (int?)null
            };

            await _booksService.UpdateBookAsync(id, updateBookModel, null, cancellationToken);

            Response.Redirect("Default.aspx");
        }
    }
}