<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Contacts.ascx.cs" Inherits="WebUserControls_Contacts" %>
<%@ Register Src="ContactDetails.ascx" TagName="ContactDetails" TagPrefix="quli" %>
<%@ Register Src="ContactsList.ascx" TagName="ContactsList" TagPrefix="quli" %>
<%@ Register Src="Activities.ascx" TagName="Activities" TagPrefix="quli" %>
<%@ Register Src="TabsTree.ascx" TagName="TabsTree" TagPrefix="quli" %>
<!-- 
табы 
-->
<quli:TabsTree ID="TabsTreeCn1" runat="server" NameTabNew="Создать контакт" NameTabDetail="Детализация"
    NameTabListing="Список контактов" OnClickListing="TabsTreeCn1_ClickListing" OnClickNew="TabsTreeCn1_ClickNew"
    WidthTabDetail="150" WidthTabListing="150" WidthTabNew="150" />
<asp:Table ID="TableTabsCn" runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0"
    Width="100%">
    <asp:TableRow>
        <asp:TableCell Width="4px" Style="background-image: url(Images/Tabs/Tabs_Left.gif)" />
        <asp:TableCell ColumnSpan="9" Style="padding: 10px 5px 10px 5px">
            <!-- поле для основной иннформации -->
            <asp:MultiView ID="MultiViewContacts" runat="server" ActiveViewIndex="0">
                <asp:View ID="ViewContactsList" runat="server">
                    <asp:Label ID="LabelContacts1" runat="server" Text="Контакты" Font-Bold="true" />
                    <br />
                    <!-- 
                    список контакотов
                    -->
                    <quli:ContactsList ID="ContactsList1" runat="server" OnContactSelected="ContactsList_ContactSelected" />
                </asp:View>
                <asp:View ID="ViewContactsEdit" runat="server">
                    <br />
                    <!-- 
                    детализация данных контакта
                    -->
                    <quli:ContactDetails ID="ContactDetails1" runat="server" OnHideContactDetails="ContactDetails1_HideContactDetails"
                        OnAfterInsert="ContactDetails1_AfterInsert" OnAfterUpdate="ContactDetails1_AfterUpdate"
                        OnBeforeInsert="ContactDetails1_BeforeInsert" OnBeforeUpdate="ContactDetails1_BeforeUpdate" />
                    <br />
                    <quli:Activities ID="Activities1" runat="server" BindContactID="0" BindTaskID="0" />
                </asp:View>
            </asp:MultiView>
            <!--  конец поля -->
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
