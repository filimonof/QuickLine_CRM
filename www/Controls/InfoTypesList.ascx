<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfoTypesList.ascx.cs"
    Inherits="Controls_InfoTypesList" %>
<!-- 
Дата Сурс Списка типов
-->
<asp:ObjectDataSource ID="ObjectDataSourceInfoType" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetData" TypeName="DataSet1TableAdapters.InfoTypeTableAdapter"></asp:ObjectDataSource>
<!-- 
Грид
 -->
<asp:GridView ID="GridViewInfoTypesList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    DataSourceID="ObjectDataSourceInfoType" DataKeyNames="ID" Width="100%" AllowPaging="False"
    OnSelectedIndexChanged="GridViewInfoTypesList_SelectedIndexChanged">
    <Columns>
        <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Buttons/edit.gif" ShowSelectButton="True" />
        <asp:BoundField DataField="Name" HeaderText="Название" SortExpression="Name" />
        <asp:BoundField DataField="TypeRelation" HeaderText="Набор данных" SortExpression="TypeRelation" />
        <asp:BoundField DataField="TypeInfo" HeaderText="Тип" SortExpression="TypeInfo" />
        <asp:BoundField DataField="NumSorted" HeaderText="Порядок" SortExpression="NumSorted" />
        <asp:CheckBoxField DataField="Enabled" HeaderText="Используется" SortExpression="Enabled" />
    </Columns>
    <EmptyDataTemplate>
        <br />
        <b>Дополнительных типов не введено</b>
        <br />
    </EmptyDataTemplate>
</asp:GridView>
