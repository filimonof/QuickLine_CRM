<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddonInfoEdit.ascx.cs"
    Inherits="Controls_AddonInfoEdit" %>
<%--
Данные
--%>
<asp:ObjectDataSource ID="ObjectDataSourceAddonEdit" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetData" TypeName="DataSet1TableAdapters.ClientInfo_SelectTableAdapter">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="X_ID" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<%--
Грид с данными
--%>
<asp:GridView ID="GridViewAddonEdit" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSourceAddonEdit"
    ShowHeader="False" OnRowDataBound="GridViewAddonEdit_RowDataBound" Width="100%">
    <Columns>
        <asp:BoundField DataField="Name" SortExpression="Name" ItemStyle-Width="250" ItemStyle-ForeColor="Black"
            ItemStyle-Font-Bold="true" />
        <asp:TemplateField SortExpression="Value">
            <ItemTemplate>
                <asp:TextBox ID="ValueString" runat="server" Text='<%# Eval("Value") %>' Visible='<%# TypeInfo.IsString(Eval("TypeInfo")) %>'></asp:TextBox>
                <asp:TextBox ID="ValueInt" runat="server" Text='<%# Eval("Value") %>' Visible='<%# TypeInfo.IsInt(Eval("TypeInfo")) %>'></asp:TextBox>
                <asp:CheckBox ID="ValueBool" runat="server" Checked='<%# TypeInfo.ToBool(Eval("Value"),false) %>'
                    Visible='<%# TypeInfo.IsBool(Eval("TypeInfo")) %>' />
                <%-- DropDownList биндится на RowDataBound --%>
                <asp:DropDownList ID="ValueCombo" runat="server" Visible="false" />
                <asp:TextBox ID="ValueTel" runat="server" Text='<%# Eval("Value") %>' Visible='<%# TypeInfo.IsTel(Eval("TypeInfo")) %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Literal ID="TypeInfo" runat="server" Text='<%# Eval("TypeInfo") %>' Visible="false"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="X_ID" runat="server" Text='<%# Eval("X_ID") %>' Visible="false"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Literal ID="X_InfoTypeID" runat="server" Text='<%# Eval("X_InfoTypeID") %>'
                    Visible="false"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
