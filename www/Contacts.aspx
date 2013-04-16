<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Contacts.aspx.cs" Inherits="Contacts" %>

<%@ Register Src="Controls/Contacts.ascx" TagName="Contacts" TagPrefix="quli" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderDesktop" runat="Server">
    <quli:Contacts ID="Contacts1" runat="server" BindClientID="0" />
</asp:Content>
