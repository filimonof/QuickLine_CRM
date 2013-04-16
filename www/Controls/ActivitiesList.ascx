<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActivitiesList.ascx.cs"
    Inherits="Controls_ActivitiesList" %>
<%--
данные
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
грид
--%>
<asp:GridView ID="GridViewActivitiesList" runat="server" AutoGenerateColumns="False"
    DataKeyNames="ID" DataSourceID="ObjectDataSourceActivitiesList" Width="100%"
    AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="GridViewActivitiesList_SelectedIndexChanged">
    <Columns>
        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Buttons/view.gif" ShowSelectButton="True" />
        <asp:BoundField DataField="LastModificationDateTime" HeaderText="Последнее изменение"
            SortExpression="LastModificationDateTime" />
        <asp:BoundField DataField="Number" HeaderText="Номер" SortExpression="Number" />
        <asp:BoundField DataField="Name" HeaderText="Название действия" SortExpression="Name" />
        <asp:BoundField DataField="TypeName" HeaderText="Тип действия" SortExpression="TypeName" />
        <asp:BoundField DataField="TaskName" HeaderText="Задача" SortExpression="TaskName" />
        <asp:BoundField DataField="ContactName" HeaderText="Контакт" SortExpression="ContactName" />
        <asp:BoundField DataField="ClientName" HeaderText="Клиент" SortExpression="ClientName" />
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>Нет действий</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
