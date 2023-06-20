<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="DeleteBook.aspx.cs" Inherits="BookShelf.WebForms.Pages.DeleteBook" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section>
            <asp:HiddenField runat="server" ID="IdHiddenField"/>

            <h1>Confirmation</h1>
            <p>Are you sure you want to delete the book record? <br /> It cannot be restored.</p>
            
            <div class="card w-50 m-3">
                <div class="card-img-top m-3"><i class="fa-solid fa-book fa-2xl"></i></div>
                <div class="card-body">
                    <h4 class="card-title">
                        <asp:Label runat="server" ID="TitleLabel"></asp:Label>
                    </h4>
                    <p class="card-text">
                        <div>
                            <asp:Label runat="server" ID="AuthorLabel" CssClass="fst-italic" />
                        </div>
                        <div>
                            <span>ISBN:</span><asp:Label runat="server" ID="ISBNLabel" />
                            <br />
                            <span>Publication year:</span><asp:Label runat="server" ID="PublicationYearLabel" />
                            <br />
                            <span>Quantity:</span><asp:Label runat="server" ID="QuantityLabel" />
                        </div>
                    </p>
                </div>
            </div>
            
            <asp:Button runat="server" ID="DeleteButton" Text="Delete" CssClass="btn btn-danger" OnClick="DeleteButton_OnClick"/>
        </section>
    </main>

</asp:Content>

