<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactsList.ascx.cs"
    Inherits="WebUserControls_ContactsList" %>
<%--
данные
--%>
<asp:ObjectDataSource ID="ObjectDataSourceContacts" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataToClientID" TypeName="DataSet1TableAdapters.ContactsTableAdapter">
    <SelectParameters>
        <asp:Parameter Name="ClientID" Type="Int32" DefaultValue="0" />
    </SelectParameters>
</asp:ObjectDataSource>
<%--
грид
--%>
<asp:GridView ID="GridViewContacts" runat="server" DataSourceID="ObjectDataSourceContacts"
    DataKeyNames="ID" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
    Width="100%" OnSelectedIndexChanged="GridViewContacts_SelectedIndexChanged">
    <Columns>
        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Buttons/view.gif" ShowSelectButton="True" />
        <asp:BoundField DataField="Number" SortExpression="Number" HeaderText="Номер" />
        <asp:BoundField DataField="Nik" SortExpression="Nik" HeaderText="Имя контакта" />
        <asp:CheckBoxField DataField="Enabled" SortExpression="Enabled" HeaderText="Активен" />
        <asp:BoundField DataField="LastModificationDateTime" SortExpression="LastModificationDateTime"
            HeaderText="Последнее изменение" />
        <asp:BoundField DataField="NameClient" SortExpression="NameClient" HeaderText="Имя клиента" />
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>Нет контактов</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
