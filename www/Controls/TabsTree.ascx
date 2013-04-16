<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabsTree.ascx.cs" Inherits="Controls_TabsTwo" %>
<asp:Table ID="TableTabs" runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0"
    Width="100%">
    <asp:TableRow>
        <%--1--%>
        <asp:TableCell ID="TabLeft1" runat="server" Width="4px">
            <asp:Image ID="ImageTabLeft1" runat="server" ImageUrl="~/Images/Tabs/Tabs_Active_Left.gif" /></asp:TableCell>
        <asp:TableCell ID="TabFon1" runat="server" Width="80px" Style="background-image: url(Images/Tabs/Tabs_Active_Fon.gif)"
            HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false">
            <asp:LinkButton ID="LinkTab1" runat="server" Font-Bold="true" ForeColor="#594f31"
                Text="Закладка1" OnClick="LinkTab1_Click" /></asp:TableCell>
        <asp:TableCell ID="TabRight1" runat="server" Width="4px">
            <asp:Image ID="ImageTabRight1" runat="server" ImageUrl="~/Images/Tabs/Tabs_Active_Right.gif" /></asp:TableCell>
        <%--2--%>
        <asp:TableCell ID="TabLeft2" runat="server" Width="4px">
            <asp:Image ID="ImageTabLeft2" runat="server" ImageUrl="~/Images/Tabs/Tabs_Deactive_Left.gif" /></asp:TableCell>
        <asp:TableCell ID="TabFon2" runat="server" Width="80px" Style="background-image: url(Images/Tabs/Tabs_Deactive_Fon.gif)"
            HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false">
            <asp:LinkButton ID="LinkTab2" runat="server" Font-Bold="false" ForeColor="#594f31"
                Text="Закладка2" OnClick="LinkTab2_Click" /></asp:TableCell>
        <asp:TableCell ID="TabRight2" runat="server" Width="4px">
            <asp:Image ID="ImageTabRight2" runat="server" ImageUrl="~/Images/Tabs/Tabs_Deactive_Right.gif" /></asp:TableCell>
        <%--3--%>
        <asp:TableCell ID="TabLeft3" runat="server" Width="4px">
            <asp:Image ID="ImageTabLeft3" runat="server" ImageUrl="~/Images/Tabs/Tabs_Deactive_Left.gif" /></asp:TableCell>
        <asp:TableCell ID="TabFon3" runat="server" Width="80px" Style="background-image: url(Images/Tabs/Tabs_Deactive_Fon.gif)"
            HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false">
            <asp:LinkButton ID="LinkTab3" runat="server" Font-Bold="false" ForeColor="#594f31"
                Text="Закладка3" /></asp:TableCell>
        <asp:TableCell ID="TabRight3" runat="server" Width="4px">
            <asp:Image ID="ImageTabRight3" runat="server" ImageUrl="~/Images/Tabs/Tabs_Deactive_Right.gif" /></asp:TableCell>
        <%--fon--%>
        <asp:TableCell ColumnSpan="2" Style="background-image: url(Images/Tabs/Tabs_Up.gif)" />
    </asp:TableRow>
    <%--
    <asp:TableRow>
        <asp:TableCell Width="4px" Style="background-image: url(Images/Tabs/Tabs_Left.gif)" />
        <asp:TableCell ColumnSpan="9" Style="padding: 10px 5px 10px 5px">
            <!-- поле для содержимого закладок -->
            
            <!-- конец поля -->
         </asp:TableCell>
        <asp:TableCell Width="4px" Style="background-image: url(Images/Tabs/Tabs_Right.gif)" />
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell Width="4px" Height="4px">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Tabs/Tabs_DownLeft.gif" /></asp:TableCell>
        <asp:TableCell ColumnSpan="9" Height="4px" Style="background-image: url(Images/Tabs/Tabs_Down.gif)" />
        <asp:TableCell Width="4px" Height="4px">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Tabs/Tabs_DownRight.gif" /></asp:TableCell>
    </asp:TableRow>
    --%>
</asp:Table>
