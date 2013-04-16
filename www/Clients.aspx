<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Clients.aspx.cs" Inherits="Clients" %>

<%@ Register Src="Controls/Clients.ascx" TagName="Clients" TagPrefix="quli" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderDesktop" runat="Server">
    <quli:Clients ID="Clients1" runat="server" />
</asp:Content>
