<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ClientsList.ascx.cs" Inherits="Controls_ClientsList" %>
<%--
данные
--%>
<asp:ObjectDataSource ID="ObjectDataSourceClients" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetData" TypeName="DataSet1TableAdapters.ClientsTableAdapter"></asp:ObjectDataSource>
<%--
грид
--%>
<asp:GridView ID="GridViewClient" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ObjectDataSourceClients"
    Width="100%" OnSelectedIndexChanged="GridViewClient_SelectedIndexChanged">
    <Columns>
        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Buttons/view.gif" ShowSelectButton="True" />
        <asp:BoundField DataField="Number" SortExpression="Number" HeaderText="Номер"></asp:BoundField>
        <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Имя"></asp:BoundField>
        <asp:CheckBoxField DataField="Enabled" SortExpression="Enabled" HeaderText="Активен">
        </asp:CheckBoxField>
        <asp:BoundField DataField="LastModificationDateTime" SortExpression="LastModificationDateTime"
            HeaderText="Последнее изменение"></asp:BoundField>
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>Нет клиентов</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
