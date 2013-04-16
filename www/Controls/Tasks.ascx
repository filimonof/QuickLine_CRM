<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tasks.ascx.cs" Inherits="Controls_Tasks" %>
<%@ Register Src="TaskDetails.ascx" TagName="TaskDetails" TagPrefix="quli" %>
<%@ Register Src="TasksList.ascx" TagName="TasksList" TagPrefix="quli" %>
<%@ Register Src="Activities.ascx" TagName="Activities" TagPrefix="quli" %>
<%@ Register Src="TabsTree.ascx" TagName="TabsTree" TagPrefix="quli" %>
<!-- 
табы 
-->
<quli:TabsTree ID="TabsTreeT1" runat="server" NameTabNew="Создать задачу" NameTabDetail="Детализация"
    NameTabListing="Список задач" OnClickListing="TabsTreeT1_ClickListing" OnClickNew="TabsTreeT1_ClickNew"
    WidthTabDetail="150" WidthTabListing="150" WidthTabNew="150" />
<asp:Table ID="TableTabsT" runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0"
    Width="100%">
    <asp:TableRow>
        <asp:TableCell Width="4px" Style="background-image: url(Images/Tabs/Tabs_Left.gif)" />
        <asp:TableCell ColumnSpan="9" Style="padding: 10px 5px 10px 5px">
            <!-- поле для содержимого закладок -->
            <asp:MultiView ID="MultiViewTasks" runat="server" ActiveViewIndex="0">
                <asp:View ID="ViewTasksList" runat="server">
                    <asp:Label ID="LabelTasks1" runat="server" Text="Задачи" Font-Bold="true" />
                    <br />
                    <!-- 
                    список задач
                    -->
                    <quli:TasksList ID="TasksList1" runat="server" BindClientID="0" OnTaskSelected="TasksList1_TaskSelected" />
                </asp:View>
                <asp:View ID="ViewTasksEdit" runat="server">
                    <br />
                    <!-- 
                    детализация данных задачи
                    -->
                    <quli:TaskDetails ID="TaskDetails1" runat="server" BindTaskID="0" OnHideTaskDetails="TaskDetails1_HideTaskDetails"
                        OnAfterInsert="TaskDetails1_AfterInsert" OnAfterUpdate="TaskDetails1_AfterUpdate"
                        OnBeforeInsert="TaskDetails1_BeforeInsert" OnBeforeUpdate="TaskDetails1_BeforeUpdate" />
                    <br />
                    <quli:Activities ID="Activities2" runat="server" BindContactID="0" BindTaskID="0" />
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
