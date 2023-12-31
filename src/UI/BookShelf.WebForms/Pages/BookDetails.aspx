﻿<%@ Page Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="BookShelf.WebForms.Pages.BookDetails" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row">
            <asp:HiddenField runat="server" ID="IdHiddenField"/>
            <h1>
                <asp:Label Id="BookTitleLabel" runat="server" Visible="True"></asp:Label>
            </h1>
            <p class="lead">Update book details:</p>

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
                <asp:Button ID="UpdateButton" runat="server" Text="Update" OnClick="UpdateBookOnClick" CssClass="btn btn-success"/>
            </div>
        </section>
    </main>

</asp:Content>
