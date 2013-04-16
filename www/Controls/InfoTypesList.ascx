<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfoTypesList.ascx.cs"
    Inherits="Controls_InfoTypesList" %>
<!-- 
���� ���� ������ �����
-->
<asp:ObjectDataSource ID="ObjectDataSourceInfoType" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetData" TypeName="DataSet1TableAdapters.InfoTypeTableAdapter"></asp:ObjectDataSource>
<!-- 
����
 -->
<asp:GridView ID="GridViewInfoTypesList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    DataSourceID="ObjectDataSourceInfoType" DataKeyNames="ID" Width="100%" AllowPaging="False"
    OnSelectedIndexChanged="GridViewInfoTypesList_SelectedIndexChanged">
    <Columns>
        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Buttons/edit.gif" ShowSelectButton="True" />
        <asp:BoundField DataField="Name" HeaderText="��������" SortExpression="Name" />
        <asp:BoundField DataField="TypeRelation" HeaderText="����� ������" SortExpression="TypeRelation" />
        <asp:BoundField DataField="TypeInfo" HeaderText="���" SortExpression="TypeInfo" />
        <asp:BoundField DataField="NumSorted" HeaderText="�������" SortExpression="NumSorted" />
        <asp:CheckBoxField DataField="Enabled" HeaderText="������������" SortExpression="Enabled" />
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>�������������� ����� �� �������</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
