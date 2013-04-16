<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActivitieType.ascx.cs"
    Inherits="Controls_ActivitieTypel" %>
<%--
данные
--%>
<asp:ObjectDataSource ID="ObjectDataSourceActivitieType" runat="server" DeleteMethod="Delete"
    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
    TypeName="DataSet1TableAdapters.ActivitieTypeTableAdapter" UpdateMethod="Update">
    <DeleteParameters>
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="NumSorted" Type="Int32" />
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </UpdateParameters>
    <InsertParameters>
        <asp:ControlParameter ControlID="TextBoxNewType" DefaultValue="" Name="Name" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter ControlID="TextBoxNewNum" DefaultValue="100" Name="NumSorted"
            PropertyName="Text" Type="int32" />
    </InsertParameters>
</asp:ObjectDataSource>
<%--
грид
--%>
<asp:GridView ID="GridViewActivitieType" runat="server" AutoGenerateColumns="False"
    DataKeyNames="ID" DataSourceID="ObjectDataSourceActivitieType" AllowSorting="true">
    <Columns>
        <asp:CommandField ButtonType="Image" ShowDeleteButton="true" DeleteImageUrl="~/Images/Buttons/del.gif"
            ShowEditButton="true" EditImageUrl="~/Images/Buttons/edit.gif" CancelImageUrl="~/Images/Buttons/cancel.gif"
            UpdateImageUrl="~/Images/Buttons/save.gif" />
        <asp:BoundField DataField="Name" HeaderText="Название активности" SortExpression="Name" />
        <asp:BoundField DataField="NumSorted" HeaderText="Порядок сотрировки" SortExpression="NumSorted" />
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>Нет типов действия</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
<br />
<asp:Label ID="Label1" runat="server" Text="Новый тип действия" />
&nbsp;
<asp:TextBox ID="TextBoxNewType" runat="server" />
&nbsp;&nbsp;&nbsp;
<asp:Label ID="Label2" runat="server" Text="порядок сортировки" />
&nbsp;
<asp:TextBox ID="TextBoxNewNum" runat="server" />
&nbsp;&nbsp;&nbsp;
<asp:Button ID="ButtonAddNewType" runat="server" Text="добавить" OnClick="ButtonAddNewType_Click" />
