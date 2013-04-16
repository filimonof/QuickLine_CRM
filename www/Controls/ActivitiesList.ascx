<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActivitiesList.ascx.cs"
    Inherits="Controls_ActivitiesList" %>
<%--
������
--%>
<asp:ObjectDataSource ID="ObjectDataSourceActivitiesList" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataToClientContactTask" TypeName="DataSet1TableAdapters.ActivitiesTableAdapter">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="ContactID" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="TaskID" Type="Int32" />
        <asp:Parameter DefaultValue="0" Name="ClientID" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<%--
����
--%>
<asp:GridView ID="GridViewActivitiesList" runat="server" AutoGenerateColumns="False"
    DataKeyNames="ID" DataSourceID="ObjectDataSourceActivitiesList" Width="100%"
    AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="GridViewActivitiesList_SelectedIndexChanged">
    <Columns>
        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Buttons/view.gif" ShowSelectButton="True" />
        <asp:BoundField DataField="LastModificationDateTime" HeaderText="��������� ���������"
            SortExpression="LastModificationDateTime" />
        <asp:BoundField DataField="Number" HeaderText="�����" SortExpression="Number" />
        <asp:BoundField DataField="Name" HeaderText="�������� ��������" SortExpression="Name" />
        <asp:BoundField DataField="TypeName" HeaderText="��� ��������" SortExpression="TypeName" />
        <asp:BoundField DataField="TaskName" HeaderText="������" SortExpression="TaskName" />
        <asp:BoundField DataField="ContactName" HeaderText="�������" SortExpression="ContactName" />
        <asp:BoundField DataField="ClientName" HeaderText="������" SortExpression="ClientName" />
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>��� ��������</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
