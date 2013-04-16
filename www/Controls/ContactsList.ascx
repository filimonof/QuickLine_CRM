<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactsList.ascx.cs"
    Inherits="WebUserControls_ContactsList" %>
<%--
������
--%>
<asp:ObjectDataSource ID="ObjectDataSourceContacts" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataToClientID" TypeName="DataSet1TableAdapters.ContactsTableAdapter">
    <SelectParameters>
        <asp:Parameter Name="ClientID" Type="Int32" DefaultValue="0" />
    </SelectParameters>
</asp:ObjectDataSource>
<%--
����
--%>
<asp:GridView ID="GridViewContacts" runat="server" DataSourceID="ObjectDataSourceContacts"
    DataKeyNames="ID" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
    Width="100%" OnSelectedIndexChanged="GridViewContacts_SelectedIndexChanged">
    <Columns>
        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Buttons/view.gif" ShowSelectButton="True" />
        <asp:BoundField DataField="Number" SortExpression="Number" HeaderText="�����" />
        <asp:BoundField DataField="Nik" SortExpression="Nik" HeaderText="��� ��������" />
        <asp:CheckBoxField DataField="Enabled" SortExpression="Enabled" HeaderText="�������" />
        <asp:BoundField DataField="LastModificationDateTime" SortExpression="LastModificationDateTime"
            HeaderText="��������� ���������" />
        <asp:BoundField DataField="NameClient" SortExpression="NameClient" HeaderText="��� �������" />
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>��� ���������</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
