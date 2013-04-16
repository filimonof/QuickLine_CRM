<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="InfoType.aspx.cs" Inherits="InfoType" %>

<%@ Register Src="Controls/InfoTypes.ascx" TagName="InfoTypes" TagPrefix="quli" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderDesktop" runat="Server">
    <%--<quli:InfoTypes ID="InfoTypes1" runat="server" />--%>
    <br />
    <br />
    <!-- 
        ���� ���� ������ �����
    -->
    <asp:ObjectDataSource ID="ObjectDataSourceInfoType" runat="server" DeleteMethod="Delete"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
        TypeName="DataSet1TableAdapters.InfoTypeTableAdapter" UpdateMethod="Update"></asp:ObjectDataSource>
    <!-- 
        ���� ���� ��� �������������� ����
    -->
    <asp:ObjectDataSource ID="ObjectDataSourceInfoTypeToID" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataToID" TypeName="DataSet1TableAdapters.InfoTypeTableAdapter"
        DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" OnInserted="ObjectDataSourceInfoTypeToID_Inserted">
        <selectparameters>
            <asp:ControlParameter ControlID="GridViewList" Name="ID" PropertyName="SelectedValue"
                Type="Int32" />
        </selectparameters>
        <deleteparameters>
            <asp:Parameter Name="Original_ID" Type="Int32" />
        </deleteparameters>
        <updateparameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="TypeInfo" Type="String" />
            <asp:Parameter Name="NumSorted" Type="Int32" />
            <asp:Parameter Name="Enabled" Type="Boolean" />
            <asp:Parameter Name="TypeRelation" Type="String" />
            <asp:Parameter Name="Original_ID" Type="Int32" />
        </updateparameters>
        <insertparameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="TypeInfo" Type="String" />
            <asp:Parameter Name="NumSorted" Type="Int32" DefaultValue="100" />
            <asp:Parameter Name="Enabled" Type="Boolean" />
            <asp:Parameter Name="TypeRelation" Type="String" />      
            <asp:Parameter Name="NewID" Type="Int32" DefaultValue="0" />      
        </insertparameters>
    </asp:ObjectDataSource>
    <!--    
        ���� ���� � ���������� ������ ���������� TypeInfo.cs
    -->
    <asp:ObjectDataSource ID="ObjectDataSourceType" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="List" TypeName="TypeInfo"></asp:ObjectDataSource>
    <!--    
        ���� ���� � ������� ������ TypeRelation.cs
    -->
    <asp:ObjectDataSource ID="ObjectDataSourceRelation" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="List" TypeName="TypeRelation"></asp:ObjectDataSource>
    <!-- 
        ����     
    -->
    <asp:MultiView ID="MultiViewInfoType" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewList" runat="server">
            <asp:Button ID="ButtonAddType" runat="server" Text="����� ���" OnClick="ButtonAddType_Click" />
            <br />
            <!--
                ���� �� ������� �����
            -->
            <asp:GridView ID="GridViewList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                DataSourceID="ObjectDataSourceInfoType" DataKeyNames="ID" Width="100%" AllowPaging="False"
                OnSelectedIndexChanged="GridViewList_SelectedIndexChanged">
                <emptydatatemplate>
                    <center>�������������� ����� �� �������</center>
                </emptydatatemplate>
                <columns>
                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Images/Buttons/edit.gif" ShowSelectButton="True" />
                    <asp:BoundField DataField="Name" HeaderText="��������" SortExpression="Name" />
                    <asp:TemplateField HeaderText="����� ������" SortExpression="TypeRelation">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListGridRelation" runat="server" DataSourceID="ObjectDataSourceRelation"
                                SelectedValue='<%# Bind("TypeRelation") %>' Width="150px">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelGridRelation" runat="server" Text='<%# Bind("TypeRelation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="���" SortExpression="TypeInfo">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListGridType" runat="server" DataSourceID="ObjectDataSourceType"
                                SelectedValue='<%# Bind("TypeInfo") %>' Width="150px">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelGridType" runat="server" Text='<%# Bind("TypeInfo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NumSorted" HeaderText="�������" SortExpression="NumSorted" />
                    <asp:CheckBoxField DataField="Enabled" HeaderText="������������" SortExpression="Enabled" />
                </columns>
            </asp:GridView>
        </asp:View>
        <asp:View ID="ViewEdit" runat="server">
            <asp:Button ID="ButtonPost" runat="server" Text="���������" OnClick="ButtonPost_Click" />
            <asp:Button ID="ButtonCancel" runat="server" Text="��������" OnClick="ButtonCancel_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="ButtonDel" runat="server" Text="�������" OnClick="ButtonDel_Click" />
            <br />
            <!--
                ������ ��� ��������������
            -->
            <asp:DetailsView ID="DetailsViewTypeInfo" runat="server" Height="50px" Width="100%"
                AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="ObjectDataSourceInfoTypeToID">
                <fields>
                    <asp:BoundField DataField="Name" HeaderText="������������" SortExpression="Name" />
                    <asp:CheckBoxField DataField="Enabled" HeaderText="������������" SortExpression="Enabled" />
                    <asp:TemplateField HeaderText="��� ����������" SortExpression="TypeInfo">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListTypeInfo" runat="server" DataSourceID="ObjectDataSourceType"
                                SelectedValue='<%# Bind("TypeInfo") %>' AutoPostBack="true" OnDataBound="DropDownListTypeInfo_DataBound"
                                OnSelectedIndexChanged="DropDownListTypeInfo_SelectedIndexChanged" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelTypeInfo" runat="server" Text='<%# Eval("TypeInfo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����� ������" SortExpression="TypeRelation">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListTypeRelation" runat="server" DataSourceID="ObjectDataSourceRelation"
                                SelectedValue='<%# Bind("TypeRelation") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelTypeRelation" runat="server" Text='<%# Eval("TypeRelation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NumSorted" HeaderText="������� ����������" SortExpression="NumSorted" />
                </fields>
            </asp:DetailsView>
            <!-- 
                ���� ���� ��� ����������
            -->
            <asp:ObjectDataSource ID="ObjectDataSourceCombo" runat="server" DeleteMethod="Delete"
                InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                TypeName="DataSet1TableAdapters.InfoTypeComboTableAdapter" UpdateMethod="Update">
                <deleteparameters>
                    <asp:Parameter Name="Original_ID" Type="Int32" />
                </deleteparameters>
                <updateparameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="NumSorted" Type="Int32" />
                    <asp:Parameter Name="Original_ID" Type="Int32" />
                </updateparameters>
                <selectparameters>
                    <asp:ControlParameter ControlID="DetailsViewTypeInfo" Name="InfoTypeID" PropertyName="SelectedValue"
                        Type="Int32" />
                </selectparameters>
                <insertparameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="NumSorted" Type="Int32" />
                    <asp:Parameter Name="InfoTypeID" Type="Int32" />
                </insertparameters>
            </asp:ObjectDataSource>
            <!-- 
                ������ �� ����������
            -->
            <asp:Label ID="LabelHeaderCombo" runat="server" Text="�������� �������� "></asp:Label>
            <asp:Repeater ID="RepeaterCombo" runat="server">
                <itemtemplate>
                    <br />
                    <asp:Button ID="ButtonDelCombo" runat="server" Text="�������" OnClick="ButtonDelCombo_Click" />
                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="��������-" />
                    <asp:TextBox ID="TextBoxCombo" runat="server" Text='<%# Eval("Name") %>' />
                    &nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="�����-" />
                    <asp:TextBox ID="TextBoxComboNum" runat="server" Text='<%# Eval("NumSorted") %>' />
                    <asp:Literal ID="LiteralID" runat="server" Text='<%# Eval("ID") %>' Visible="false" />
                </itemtemplate>
            </asp:Repeater>
            <br />
            <asp:Label ID="LabelAddCombo" runat="server" Text="��������-" />
            <asp:TextBox ID="TextBoxAddCombo" runat="server" />
            &nbsp;&nbsp;&nbsp;<asp:Label ID="LabelAddComboNum" runat="server" Text="�����-" />
            <asp:TextBox ID="TextBoxAddComboNum" runat="server" />
            <asp:Button ID="ButtonAddCombo" runat="server" Text="��������" OnClick="ButtonAddCombo_Click" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
