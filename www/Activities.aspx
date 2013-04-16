<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Activities.aspx.cs" Inherits="Activities" %>

<%@ Register Src="Controls/Activities.ascx" TagName="Activities" TagPrefix="quli" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderDesktop" runat="Server">
    <quli:Activities ID="Activities1" runat="server" />
</asp:Content>
