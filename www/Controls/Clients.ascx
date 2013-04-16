<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Clients.ascx.cs" Inherits="Controls_Clients" %>
<%@ Register Src="ClientsList.ascx" TagName="ClientsList" TagPrefix="quli" %>
<%@ Register Src="ClientDetails.ascx" TagName="ClientDetails" TagPrefix="quli" %>
<%@ Register Src="Contacts.ascx" TagName="Contacts" TagPrefix="quli" %>
<%@ Register Src="Tasks.ascx" TagName="Tasks" TagPrefix="quli" %>
<%@ Register Src="Activities.ascx" TagName="Activities" TagPrefix="quli" %>
<%@ Register Src="TabsTree.ascx" TagName="TabsTree" TagPrefix="quli" %>
<!-- 
табы 
-->
<quli:TabsTree ID="TabsTreeC1" runat="server" NameTabNew="Создать клиента" NameTabDetail="Детализация"
    NameTabListing="Список клиентов" OnClickListing="TabsTreeC1_ClickListing" OnClickNew="TabsTreeC1_ClickNew"
    WidthTabDetail="150" WidthTabListing="150" WidthTabNew="150" />
<asp:Table ID="TableTabsC" runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0"
    Width="100%">
    <asp:TableRow>
        <asp:TableCell Width="4px" Style="background-image: url(Images/Tabs/Tabs_Left.gif)" />
        <asp:TableCell ColumnSpan="9" Style="padding: 10px 5px 10px 5px">
            <!-- поле для основной иннформации -->
            <asp:MultiView ID="MultiViewClients" runat="server" ActiveViewIndex="0">
                <asp:View ID="ViewClientsList" runat="server">
                    <asp:Label ID="LabelClients1" runat="server" Text="Клиенты" Font-Bold="true" />
                    <br />
                    <!-- 
                    список клиентов
                    -->
                    <quli:ClientsList ID="ClientsList1" runat="server" OnClientSelected="ClientsList_ClientSelected" />
                </asp:View>
                <asp:View ID="ViewClientsEdit" runat="server">
                    <br />
                    <!-- 
                    детализация данных клиента
                    -->
                    <quli:ClientDetails ID="ClientDetails1" runat="server" OnHideClientDetails="ClientDetails1_HideClientDetails"
                        OnAfterInsert="ClientDetails1_AfterInsert" OnAfterUpdate="ClientDetails1_AfterUpdate"
                        OnBeforeInsert="ClientDetails1_BeforeInsert" OnBeforeUpdate="ClientDetails1_BeforeUpdate" />
                    <br />
                    <quli:Contacts ID="Contacts1" runat="server" BindClientID="0" />
                    <br />
                    <quli:Tasks ID="Tasks1" runat="server" BindClientID="0" />
                    <br />
                    <quli:Activities ID="Activities3" runat="server" BindContactID="0" BindTaskID="0" />
                </asp:View>
            </asp:MultiView>
            <!-- конец поля -->
        </asp:TableCell>
        <asp:TableCell Width="4px" Style="background-image: url(Images/Tabs/Tabs_Right.gif)" />
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell Width="4px" Height="4px">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Tabs/Tabs_DownLeft.gif" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="9" Height="4px" Style="background-image: url(Images/Tabs/Tabs_Down.gif)" />
        <asp:TableCell Width="4px" Height="4px">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Tabs/Tabs_DownRight.gif" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
