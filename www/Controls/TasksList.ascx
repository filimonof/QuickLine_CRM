<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TasksList.ascx.cs" Inherits="Controls_TasksList" %>
<%--
������
--%>
<asp:ObjectDataSource ID="ObjectDataSourceTasks" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataToClientID" TypeName="DataSet1TableAdapters.TasksTableAdapter">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="ClientID" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<%--
����
--%>
<asp:GridView ID="GridViewTasks" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ObjectDataSourceTasks"
    Width="100%" OnSelectedIndexChanged="GridViewTasks_SelectedIndexChanged">
    <Columns>
        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Buttons/view.gif" ShowSelectButton="True" />
        <asp:BoundField DataField="LastModificationDateTime" HeaderText="�������� ���������"
            SortExpression="LastModificationDateTime" />
        <asp:BoundField DataField="Number" HeaderText="�����" SortExpression="Number" />
        <asp:BoundField DataField="Name" HeaderText="��������" SortExpression="Name" />
        <asp:TemplateField SortExpression="Status" HeaderText="������">
            <ItemTemplate>
                <asp:Label ID="LabelStatus" runat="server" Text='<%# TypeStatus.StatusToString(Eval("Status")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="NameClient" SortExpression="NameClient" HeaderText="��� �������" />
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>��� �����</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
