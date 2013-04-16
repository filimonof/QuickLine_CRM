<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TasksList.ascx.cs" Inherits="Controls_TasksList" %>
<%--
данные
--%>
<asp:ObjectDataSource ID="ObjectDataSourceTasks" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataToClientID" TypeName="DataSet1TableAdapters.TasksTableAdapter">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="ClientID" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<%--
грид
--%>
<asp:GridView ID="GridViewTasks" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ObjectDataSourceTasks"
    Width="100%" OnSelectedIndexChanged="GridViewTasks_SelectedIndexChanged">
    <Columns>
        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Buttons/view.gif" ShowSelectButton="True" />
        <asp:BoundField DataField="LastModificationDateTime" HeaderText="Посленее изменение"
            SortExpression="LastModificationDateTime" />
        <asp:BoundField DataField="Number" HeaderText="Номер" SortExpression="Number" />
        <asp:BoundField DataField="Name" HeaderText="Название" SortExpression="Name" />
        <asp:TemplateField SortExpression="Status" HeaderText="Статус">
            <ItemTemplate>
                <asp:Label ID="LabelStatus" runat="server" Text='<%# TypeStatus.StatusToString(Eval("Status")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="NameClient" SortExpression="NameClient" HeaderText="Имя клиента" />
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>Нет задач</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
