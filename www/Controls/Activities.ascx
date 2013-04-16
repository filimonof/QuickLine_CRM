<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Activities.ascx.cs" Inherits="Controls_Activities" %>
<%@ Register Src="ActivitiesList.ascx" TagName="ActivitiesList" TagPrefix="quli" %>
<%@ Register Src="ActivitieDetails.ascx" TagName="ActivitieDetails" TagPrefix="quli" %>
<%@ Register Src="TabsTree.ascx" TagName="TabsTree" TagPrefix="quli" %>
<!-- 
���� 
-->
<quli:TabsTree ID="TabsTreeA1" runat="server" NameTabNew="������� ��������" NameTabDetail="�����������"
    NameTabListing="������ ��������" OnClickListing="TabsTreeA1_ClickListing" OnClickNew="TabsTreeA1_ClickNew"
    WidthTabDetail="150" WidthTabListing="150" WidthTabNew="150" />
<asp:Table ID="TableTabsA" runat="server" CellPadding="0" CellSpacing="0" BorderWidth="0"
    Width="100%">
    <asp:TableRow>
        <asp:TableCell Width="4px" Style="background-image: url(Images/Tabs/Tabs_Left.gif)" />
        <asp:TableCell ColumnSpan="9" Style="padding: 10px 5px 10px 5px">
            <!-- ���� ��� �������� ����������� -->
            <asp:MultiView ID="MultiViewActivities" runat="server" ActiveViewIndex="0">
                <asp:View ID="ViewTasksActivities" runat="server">
                    <asp:Label ID="LabelActivities1" runat="server" Text="��������" Font-Bold="true" />
                    <br />
                    <!-- 
                    ������ ��������
                    -->
                    <quli:ActivitiesList ID="ActivitiesList1" runat="server" BindContactID="0" BindTaskID="0"
                        BindClientID="0" OnActivitiesSelected="ActivitiesList1_ActivitiesSelected" />
                </asp:View>
                <asp:View ID="ViewActivitiesEdit" runat="server">
                    <br />
                    <!-- 
                    ����������� ������ ��������
                    -->
                    <quli:ActivitieDetails ID="ActivitieDetails1" runat="server" BindActivitieID="0"
                        OnHideActivitieDetails="ActivitieDetails1_HideActivitieDetails" OnAfterUpdate="ActivitieDetails1_AfterUpdate"
                        OnBeforeUpdate="ActivitieDetails1_BeforeUpdate" />
                </asp:View>
            </asp:MultiView>
            <!-- ����� ���� -->
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
