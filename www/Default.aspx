<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="Controls/TabsTree.ascx" TagName="TabsTree" TagPrefix="quli" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderDesktop" runat="Server">
    <center>
        <asp:Label ID="Label1" runat="server" Text="Приветствую тебя, о Великий." Font-Bold="true" Width="100%" />
        &nbsp;&nbsp;&nbsp;        
        <br />
        <br />
        <asp:Calendar ID="Calendar1" runat="server" />
        <br />
        <asp:TextBox ID="TextBox111" runat="server" Text="Привет." />    
        &nbsp;&nbsp;&nbsp;        
        <asp:DropDownList ID="DropDownList1" runat="server"  />        
        &nbsp;&nbsp;&nbsp;        
        <asp:Button ID="Button1" runat="server" Text="Превед !!!!" />
    </center>
</asp:Content>
