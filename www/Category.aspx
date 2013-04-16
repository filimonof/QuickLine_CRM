<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Category.aspx.cs" Inherits="Category"  %>

<%@ Register Src="Controls/Category.ascx" TagName="Category" TagPrefix="quli" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderDesktop" runat="Server">
    <quli:Category ID="Category1" runat="server" />
</asp:Content>
