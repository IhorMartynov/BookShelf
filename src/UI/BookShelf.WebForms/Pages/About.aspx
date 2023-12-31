﻿<%@ Page Title="About" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="BookShelf.WebForms.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Library Management WebForms application.</h3>
        <p>A simple web application for managing a library's book inventory targeting the .NET Framework 4.8.</p>
    </main>
</asp:Content>
