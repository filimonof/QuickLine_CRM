<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfoTypeDetails.ascx.cs"
    Inherits="Controls_InfoTypeDetails" %>
<%@ Register Src="InfoTypeCombo.ascx" TagName="InfoTypeCombo" TagPrefix="quli" %>
<!-- 
данные
-->
<asp:ObjectDataSource ID="ObjectDataSourceInfoTypeToID" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataToID" TypeName="DataSet1TableAdapters.InfoTypeTableAdapter"
    DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" OnInserted="ObjectDataSourceInfoTypeToID_Inserted">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="ID" Type="Int32" />
        <%--<asp:ControlParameter ControlID="GridViewList" Name="ID" PropertyName="SelectedValue"
            Type="Int32" />--%>
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="TypeInfo" Type="String" />
        <asp:Parameter Name="NumSorted" Type="Int32" />
        <asp:Parameter Name="Enabled" Type="Boolean" />
        <asp:Parameter Name="TypeRelation" Type="String" />
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="TypeInfo" Type="String" />
        <asp:Parameter Name="NumSorted" Type="Int32" DefaultValue="100" />
        <asp:Parameter Name="Enabled" Type="Boolean" />
        <asp:Parameter Name="TypeRelation" Type="String" />
        <asp:Parameter Name="NewID" Type="Int32" DefaultValue="0" />
    </InsertParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceType" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="List" TypeName="TypeInfo"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceRelation" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="List" TypeName="TypeRelation"></asp:ObjectDataSource>
<!-- 
Кнопки
-->
<asp:Button ID="ButtonPost" runat="server" Text="Сохранить" OnClick="ButtonPost_Click" />
<asp:Button ID="ButtonCancel" runat="server" Text="Отменить" OnClick="ButtonCancel_Click" />
&nbsp;&nbsp;&nbsp;
<asp:Button ID="ButtonDel" runat="server" Text="Удалить" OnClick="ButtonDel_Click"
    OnPreRender="ButtonDel_PreRender" />
<br />
<!-- 
детализация типа
-->
<asp:DetailsView ID="DetailsViewTypeInfo" runat="server" Width="100%" AutoGenerateRows="False"
    DataKeyNames="ID" DataSourceID="ObjectDataSourceInfoTypeToID">
    <Fields>
        <asp:BoundField DataField="Name" HeaderText="Наименование" SortExpression="Name" />
        <asp:CheckBoxField DataField="Enabled" HeaderText="Используется" SortExpression="Enabled" />
        <asp:TemplateField HeaderText="Тип информации" SortExpression="TypeInfo">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListTypeInfo" runat="server" DataSourceID="ObjectDataSourceType"
                    SelectedValue='<%# Bind("TypeInfo") %>' AutoPostBack="true" OnDataBound="DropDownListTypeInfo_DataBound"
                    OnSelectedIndexChanged="DropDownListTypeInfo_SelectedIndexChanged" />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelTypeInfo" runat="server" Text='<%# Eval("TypeInfo") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Набор данных" SortExpression="TypeRelation">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListTypeRelation" runat="server" DataSourceID="ObjectDataSourceRelation"
                    SelectedValue='<%# Bind("TypeRelation") %>' />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelTypeRelation" runat="server" Text='<%# Eval("TypeRelation") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="NumSorted" HeaderText="Порядок сортировки" SortExpression="NumSorted" />
    </Fields>
    <EmptyDataTemplate>
        <br />
        <b>Тип не определён</b>
        <br />
    </EmptyDataTemplate>
</asp:DetailsView>
<%--
Combo
--%>
<quli:InfoTypeCombo ID="InfoTypeCombo1" runat="server" />
