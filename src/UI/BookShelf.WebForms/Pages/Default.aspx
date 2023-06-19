<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BookShelf.WebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row">
            <h1>Books</h1>
            <p class="lead">All library books:</p>

            <asp:GridView ID="BooksGridView" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Author" HeaderText="Author" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                    <asp:BoundField DataField="PublicationYear" HeaderText="Publication Year" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="BookDetailsLinkButton" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="BookDetailsLinkButton_Click" Text="Edit" />
                            <asp:Button ID="BookDeleteLinkButton" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="BookDeleteLinkButton_Click" Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </section>
    </main>

</asp:Content>
