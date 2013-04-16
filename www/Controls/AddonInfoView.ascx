<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddonInfoView.ascx.cs"
    Inherits="Controls_AddonInfoView" %>
<%-- 
данные
--%>
<asp:ObjectDataSource ID="ObjectDataSourceAddon" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetData" TypeName="DataSet1TableAdapters.ClientInfo_SelectTableAdapter">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="X_ID" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<%--
грид
--%>
<asp:GridView ID="GridViewAddonList" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSourceAddon"
    ShowHeader="False" OnRowDataBound="GridViewAddonList_RowDataBound" Width="100%">
    <Columns>
        <asp:BoundField DataField="Name" SortExpression="Name" ItemStyle-Width="250" ItemStyle-ForeColor="Black"
            ItemStyle-Font-Bold="true"></asp:BoundField>
        <asp:TemplateField SortExpression="Value">
            <ItemTemplate>
                <asp:Label ID="ValueStringList" runat="server" Text='<%# Eval("Value") %>' Visible='<%# TypeInfo.IsString(Eval("TypeInfo")) %>' />
                <asp:Label ID="ValueIntList" runat="server" Text='<%# Eval("Value") %>' Visible='<%# TypeInfo.IsInt(Eval("TypeInfo")) %>' />
                <asp:CheckBox ID="ValueBoolList" runat="server" Checked='<%# TypeInfo.ToBool(Eval("Value"),false) %>'
                    Visible='<%# TypeInfo.IsBool(Eval("TypeInfo")) %>' Enabled="false" />
                <asp:Label ID="ValueComboList" runat="server" Text='<%# Eval("Value") %>' Visible='<%# TypeInfo.IsCombo(Eval("TypeInfo")) %>' />
                <asp:Label ID="ValueTelList" runat="server" Text='<%# Eval("Value") %>' Visible='<%# TypeInfo.IsTel(Eval("TypeInfo")) %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="TypeInfo" SortExpression="TypeInfo"></asp:BoundField>
    </Columns>
</asp:GridView>
