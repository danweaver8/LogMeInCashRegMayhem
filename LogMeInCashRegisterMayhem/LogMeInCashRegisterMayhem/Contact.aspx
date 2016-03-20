<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="LogMeInCashRegisterMayhem.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Foo Company Contact Page.</h3>
    <address>
        Dan Weaver<br />
        155 W. Salaignac St. Philadelphia, PA<br />
        <abbr title="Phone">P:</abbr>
        609-509-6101
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Danweaver8@gmail.com">Danweaver8@gmail.com</a><br />
    </address>
</asp:Content>
