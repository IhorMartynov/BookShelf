using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using BookShelf.Application.Contracts.Services;
using BookShelf.Domain.Models;

namespace BookShelf.WebForms.Pages
{
    public partial class AddBook : Page
    {
        private readonly IBooksService _booksService;
        private readonly ICategoriesService _categoriesService;

        public AddBook(IBooksService booksService, ICategoriesService categoriesService)
        {
            _booksService = booksService;
            _categoriesService = categoriesService;
        }

        protected void Page_Load(object sender, EventArgs e) =>
            Page.RegisterAsyncTask(new PageAsyncTask(PopulateCategories));

        protected void SaveButton_OnClick(object sender, EventArgs e) =>
            Page.RegisterAsyncTask(new PageAsyncTask(SaveBook));

        private async Task SaveBook(CancellationToken cancellationToken)
        {
            var categoryId = int.TryParse(CategoryDropDownList.SelectedValue, out var value) ? value : 1;

            var addBookModel = new AddBookModel
            {
                Title = TitleTextBox.Text,
                Author = AuthorTextBox.Text,
                ISBN = ISBNTextBox.Text,
                PublicationYear = int.TryParse(PublicationYearTextBox.Text, out var year) ? year : DateTime.Now.Year,
                Quantity = int.TryParse(QuantityTextBox.Text, out var quantity) ? quantity : 0
            };

            await _booksService.AddBookAsync(addBookModel, categoryId, cancellationToken);

            Response.Redirect("Default.aspx");
        }

        private async Task PopulateCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoriesService.GetAllCategoriesAsync(cancellationToken)
                ?? Array.Empty<Category>();

            CategoryDropDownList.DataSource = categories;
            CategoryDropDownList.DataBind();
        }
    }
}