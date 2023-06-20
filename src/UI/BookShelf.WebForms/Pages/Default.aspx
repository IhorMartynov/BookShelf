<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BookShelf.WebForms._Default" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row">
            <h1>Books</h1>
            <div class="d-flex justify-content-between p-3">
                <span class="lead">All library books:</span>
                <asp:HyperLink runat="server" NavigateUrl="AddBook.aspx" CssClass="btn btn-primary"><i class="fa-solid fa-plus fa-lg"></i> Add</asp:HyperLink>
            </div>

            <asp:GridView ID="BooksGridView" runat="server" AutoGenerateColumns="false" EmptyDataText="Library is empty." CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="Author" HeaderText="Author" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                    <asp:BoundField DataField="PublicationYear" HeaderText="Publication Year" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink runat="server" NavigateUrl='<%# ("BookDetails.aspx?id=") + Eval("Id") %>' CssClass="btn btn-info" >Edit</asp:HyperLink>
                            <asp:HyperLink runat="server" NavigateUrl='<%# ("DeleteBook.aspx?id=") + Eval("Id") %>' CssClass="btn btn-danger" >Remove</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </section>
    </main>

</asp:Content>
