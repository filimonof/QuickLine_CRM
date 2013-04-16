<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfoTypeCombo.ascx.cs"
    Inherits="Controls_InfoTypeCombo" %>
<!-- 
Дата сурс для комбобокса
-->
<asp:ObjectDataSource ID="ObjectDataSourceCombo" runat="server" DeleteMethod="Delete"
    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
    TypeName="DataSet1TableAdapters.InfoTypeComboTableAdapter" UpdateMethod="Update">
    <DeleteParameters>
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="NumSorted" Type="Int32" />
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </UpdateParameters>
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="InfoTypeID" Type="Int32" />
        <%--<asp:ControlParameter ControlID="DetailsViewTypeInfo" Name="InfoTypeID" PropertyName="SelectedValue"
            Type="Int32" />--%>
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="NumSorted" Type="Int32" />
        <asp:Parameter Name="InfoTypeID" Type="Int32" />
    </InsertParameters>
</asp:ObjectDataSource>
<!-- 
данные из комбобокса
-->
<asp:Label ID="LabelHeaderCombo" runat="server" Text="Варианты значений "></asp:Label>
<asp:Repeater ID="RepeaterCombo" runat="server">
    <ItemTemplate>
        <br />
        <asp:Button ID="ButtonDelCombo" runat="server" Text="Удалить" OnClick="ButtonDelCombo_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="значение-" />
        <asp:TextBox ID="TextBoxCombo" runat="server" Text='<%# Eval("Name") %>' />
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="номер-" />
        <asp:TextBox ID="TextBoxComboNum" runat="server" Text='<%# Eval("NumSorted") %>' />
        <asp:Literal ID="LiteralID" runat="server" Text='<%# Eval("ID") %>' Visible="false" />
    </ItemTemplate>
</asp:Repeater>
<br />
<asp:Label ID="LabelAddCombo" runat="server" Text="значение-" />
<asp:TextBox ID="TextBoxAddCombo" runat="server" />
&nbsp;&nbsp;&nbsp;
<asp:Label ID="LabelAddComboNum" runat="server" Text="номер-" />
<asp:TextBox ID="TextBoxAddComboNum" runat="server" />
&nbsp;&nbsp;&nbsp;
<asp:Button ID="ButtonAddCombo" runat="server" Text="Добавить" OnClick="ButtonAddCombo_Click" />
