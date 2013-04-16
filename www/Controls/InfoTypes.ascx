<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfoTypes.ascx.cs" Inherits="Controls_InfoTypes" %>
<%@ Register Src="InfoTypesList.ascx" TagName="InfoTypesList" TagPrefix="quli" %>
<%@ Register Src="InfoTypeDetails.ascx" TagName="InfoTypeDetails" TagPrefix="quli" %>
<%@ Register Src="TabsTree.ascx" TagName="TabsTree" TagPrefix="quli" %>
<%--  
вкладки
--%>
<quli:TabsTree ID="TabsTreeIT1" runat="server" NameTabNew="Создать тип информации"
    NameTabDetail="Редактирование типа информации" NameTabListing="Список типов информации"
    OnClickListing="TabsTreeIT1_ClickListing" OnClickNew="TabsTreeIT1_ClickNew" WidthTabDetail="230"
    WidthTabListing="220" WidthTabNew="180" />
<asp:Table ID="TableTabsIT" runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0"
    Width="100%">
    <asp:TableRow>
        <asp:TableCell Width="4px" Style="background-image: url(Images/Tabs/Tabs_Left.gif)" />
        <asp:TableCell ColumnSpan="9" Style="padding: 10px 5px 10px 5px">
            <!-- поле для основной иннформации -->
            <asp:MultiView ID="MultiViewInfoType" runat="server" ActiveViewIndex="0">
                <asp:View ID="ViewList" runat="server">
                    <asp:Label ID="LabelInfoType1" runat="server" Text="Типы информации" Font-Bold="true" />
                    <br />
                    <!-- 
                    список типов
                    -->
                    <quli:InfoTypesList ID="InfoTypesList1" runat="server" OnTypeInfoSelected="InfoTypesList1_TypeInfoSelected" />
                </asp:View>
                <asp:View ID="ViewClientsEdit" runat="server">
                    <br />
                    <!-- 
                    детализация данных типа
                    -->
                    <quli:InfoTypeDetails ID="InfoTypeDetails1" runat="server" OnHideInfoTypeDetails="InfoTypeDetails1_HideInfoTypeDetails"
                        OnAfterUpdate="InfoTypeDetails1_AfterUpdate" OnBeforeUpdate="InfoTypeDetails1_BeforeUpdate" />
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
