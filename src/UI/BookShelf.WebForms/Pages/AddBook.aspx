<%@ Page Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="BookShelf.WebForms.Pages.AddBook" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row">
            <h1>New book record</h1>
            <p class="lead">Fill in all the fields:</p>

            <div>
                <div class="mb-3">
                    <asp:Label ID="TitleLabel" runat="server" Text="Title:" for="TitleTextBox" class="form-label"></asp:Label>
                    <asp:TextBox ID="TitleTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="AuthorLabel" runat="server" Text="Author:" for="AuthorTextBox" class="form-label"></asp:Label>
                    <asp:TextBox ID="AuthorTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="ISBNLabel" runat="server" Text="ISBN:" for="ISBNTextBox" class="form-label"></asp:Label>
                    <asp:TextBox ID="ISBNTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="PublicationYearLabel" runat="server" Text="Publication year:" for="PublicationYearTextBox" class="form-label"></asp:Label>
                    <asp:TextBox ID="PublicationYearTextBox" runat="server" CssClass="form-control" type="number"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="QuantityLabel" runat="server" Text="Quantity:" for="QuantityTextBox" class="form-label"></asp:Label>
                    <asp:TextBox ID="QuantityTextBox" runat="server" CssClass="form-control" type="number"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="CategoryLabel" runat="server" Text="Category:" for="CategoryDropDownList" class="form-label"></asp:Label>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="Id" />
                </div>
                <asp:Button ID="SaveButton" runat="server" Text="Update" OnClick="SaveButton_OnClick" CssClass="btn btn-success"/>
            </div>
        </section>
    </main>

</asp:Content>
