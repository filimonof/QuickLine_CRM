<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ClientDetails.ascx.cs"
    Inherits="Controls_ClientDetails" %>
<%@ Register Src="AddonInfo.ascx" TagName="AddonInfo" TagPrefix="quli" %>
<%--
������
--%>
<asp:ObjectDataSource ID="ObjectDataSourceClientDetails" runat="server" DeleteMethod="Delete"
    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataToID"
    TypeName="DataSet1TableAdapters.ClientsTableAdapter" UpdateMethod="Update" OnUpdating="ObjectDataSourceClientDetails_Updating"
    OnInserting="ObjectDataSourceClientDetails_Inserting" OnInserted="ObjectDataSourceClientDetails_Inserted">
    <DeleteParameters>
        <asp:Parameter Type="Int32" Name="Original_ID"></asp:Parameter>
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Type="String" Name="Number"></asp:Parameter>
        <asp:Parameter Type="String" Name="Name"></asp:Parameter>
        <asp:Parameter Type="Boolean" Name="Enabled"></asp:Parameter>
        <asp:Parameter Type="DateTime" Name="LastModificationDateTime"></asp:Parameter>
        <asp:Parameter Type="Int32" Name="LastModificationAgentID"></asp:Parameter>
        <asp:Parameter Type="Int32" Name="Original_ID"></asp:Parameter>
    </UpdateParameters>
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="ID" Type="Int32" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Type="String" Name="Number"></asp:Parameter>
        <asp:Parameter Type="String" Name="Name"></asp:Parameter>
        <asp:Parameter Type="Boolean" DefaultValue="true" Name="Enabled"></asp:Parameter>
        <asp:Parameter Type="DateTime" Name="CreationDateTime"></asp:Parameter>
        <asp:Parameter Type="Int32" Name="CreationAgentID"></asp:Parameter>
        <asp:Parameter Type="DateTime" Name="LastModificationDateTime"></asp:Parameter>
        <asp:Parameter Type="Int32" Name="LastModificationAgentID"></asp:Parameter>
        <asp:Parameter Name="NewID" Type="Int32" DefaultValue="0" />
    </InsertParameters>
</asp:ObjectDataSource>
<!-- 
������
-->
<asp:Button ID="ButtonEdit" runat="server" Text="�������������" OnClick="ButtonEdit_Click"
    OnPreRender="Button_PreRender" />
<asp:Button ID="ButtonDel" runat="server" Text="�������" OnClick="ButtonDel_Click"
    OnPreRender="Button_PreRender" />
<asp:Button ID="ButtonPost" runat="server" Text="���������" OnClick="ButtonPost_Click"
    OnPreRender="Button_PreRender" />
<asp:Button ID="ButtonCancel" runat="server" Text="��������" OnClick="ButtonCancel_Click"
    OnPreRender="Button_PreRender" />    
<br />   
<!-- 
����������� ������ �������
-->
<asp:DetailsView ID="DetailsViewClient" runat="server" DataKeyNames="ID" DataSourceID="ObjectDataSourceClientDetails"
    AutoGenerateRows="False">
    <Fields>
        <asp:BoundField DataField="Number" SortExpression="Number" HeaderText="�����"></asp:BoundField>
        <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="��������"></asp:BoundField>
        <asp:CheckBoxField DataField="Enabled" SortExpression="Enabled" HeaderText="�������">
        </asp:CheckBoxField>
        <asp:TemplateField SortExpression="CreationDateTime" HeaderText="���� �������� �������"
            InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelCreateDate" runat="server" Text='<%# Eval("CreationDateTime") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelCreateDate" runat="server" Text='<%# Eval("CreationDateTime") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="CreationAgentID" HeaderText="������ �����" InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelCreateAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("CreationAgentID")) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelCreateAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("CreationAgentID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="LastModificationDateTime" HeaderText="��������� ���������"
            InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelLastModifDate" runat="server" Text='<%# DateTime.Now + " (�������������)" %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelLastModifDate" runat="server" Text='<%# Eval("LastModificationDateTime") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="LastModificationAgentID" HeaderText="����� ��������� ���������"
            InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelLastModifAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("LastModificationAgentID")) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelLastModifAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("LastModificationAgentID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Fields>
    <EmptyDataTemplate>
        <br />
        <b>�� �������� ������</b>
        <br />
    </EmptyDataTemplate>
</asp:DetailsView>
<!-- 
�������������� ������
-->
<quli:AddonInfo ID="AddonInfo1" runat="server" RelationTable="client" X_ID="0" />
