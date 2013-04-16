<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Category.ascx.cs" Inherits="Controls_Category" %>
<%--
данные
--%>
<asp:ObjectDataSource ID="ObjectDataSourceCategory" runat="server" DeleteMethod="Delete"
    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
    TypeName="DataSet1TableAdapters.CategoryTableAdapter" UpdateMethod="Update">
    <DeleteParameters>
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="NumSorted" Type="Int32" />
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </UpdateParameters>
    <InsertParameters>
        <asp:ControlParameter ControlID="TextBoxNewCategory" DefaultValue="" Name="Name"
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="TextBoxNewNum" DefaultValue="100" Name="NumSorted"
            PropertyName="Text" Type="int32" />
    </InsertParameters>
</asp:ObjectDataSource>
<%--
грид
--%>
<asp:GridView ID="GridViewCategory" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
    DataSourceID="ObjectDataSourceCategory" AllowSorting="true">
    <Columns>
        <asp:CommandField ButtonType="Image" ShowDeleteButton="true" DeleteImageUrl="~/Images/Buttons/del.gif"
            ShowEditButton="true" EditImageUrl="~/Images/Buttons/edit.gif" CancelImageUrl="~/Images/Buttons/cancel.gif"
            UpdateImageUrl="~/Images/Buttons/save.gif" />
        <asp:BoundField DataField="Name" HeaderText="Название категории" SortExpression="Name" />
        <asp:BoundField DataField="NumSorted" HeaderText="Порядок" SortExpression="Name" />
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>Нет категорий</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
<br />
<asp:Label ID="Label1" runat="server" Text="Новая категория"></asp:Label>
&nbsp;
<asp:TextBox ID="TextBoxNewCategory" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
<asp:Label ID="Label2" runat="server" Text="порядок сортировки"></asp:Label>
&nbsp;
<asp:TextBox ID="TextBoxNewNum" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
<asp:Button ID="ButtonAddNewCategory" runat="server" Text="Добавить" OnClick="ButtonAddNewCategory_Click" />
