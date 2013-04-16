<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Tasks.aspx.cs" Inherits="Tasks" %>

<%@ Register Src="Controls/Tasks.ascx" TagName="Tasks" TagPrefix="quli" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderDesktop" runat="Server">
    <quli:Tasks ID="Tasks1" runat="server" />
</asp:Content>
