<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ClientsList.ascx.cs" Inherits="Controls_ClientsList" %>
<%--
������
--%>
<asp:ObjectDataSource ID="ObjectDataSourceClients" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetData" TypeName="DataSet1TableAdapters.ClientsTableAdapter"></asp:ObjectDataSource>
<%--
����
--%>
<asp:GridView ID="GridViewClient" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ObjectDataSourceClients"
    Width="100%" OnSelectedIndexChanged="GridViewClient_SelectedIndexChanged">
    <Columns>
        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Buttons/view.gif" ShowSelectButton="True" />
        <asp:BoundField DataField="Number" SortExpression="Number" HeaderText="�����"></asp:BoundField>
        <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="���"></asp:BoundField>
        <asp:CheckBoxField DataField="Enabled" SortExpression="Enabled" HeaderText="�������">
        </asp:CheckBoxField>
        <asp:BoundField DataField="LastModificationDateTime" SortExpression="LastModificationDateTime"
            HeaderText="��������� ���������"></asp:BoundField>
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>��� ��������</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
