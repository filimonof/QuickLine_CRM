<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActivitieDetails.ascx.cs"
    Inherits="Controls_ActivitieDetails" %>
<%@ Register Src="AddonInfo.ascx" TagName="AddonInfo" TagPrefix="quli" %>
<%--
������
--%>
<asp:ObjectDataSource ID="ObjectDataSourceActivitieDetails" runat="server" DeleteMethod="Delete"
    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataToID"
    TypeName="DataSet1TableAdapters.ActivitiesTableAdapter" UpdateMethod="Update"
    OnInserting="ObjectDataSourceActivitieDetails_Inserting" OnUpdating="ObjectDataSourceActivitieDetails_Updating"
    OnInserted="ObjectDataSourceActivitieDetails_Inserted">
    <DeleteParameters>
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="TaskID" Type="Int32" />
        <asp:Parameter Name="ContactID" Type="Int32" />
        <asp:Parameter Name="AgentID" Type="Int32" />
        <asp:Parameter Name="TypeID" Type="Int32" />
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="LastModificationDateTime" Type="DateTime" />
        <asp:Parameter Name="LastModificationAgentID" Type="Int32" />
        <asp:Parameter Name="StartDateTime" Type="DateTime" />
        <asp:Parameter Name="StopDateTime" Type="DateTime" />
        <asp:Parameter Name="ResultAction" Type="String" />
        <asp:Parameter Name="DateTimeNextAction" Type="DateTime" />
        <asp:Parameter Name="Number" Type="String" />
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </UpdateParameters>
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="ID" Type="Int32" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Name="TaskID" Type="Int32" />
        <asp:Parameter Name="ContactID" Type="Int32" />
        <asp:Parameter Name="AgentID" Type="Int32" />
        <asp:Parameter Name="TypeID" Type="Int32" />
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="CreationDateTime" Type="DateTime" />
        <asp:Parameter Name="CreationAgentID" Type="Int32" />
        <asp:Parameter Name="LastModificationDateTime" Type="DateTime" />
        <asp:Parameter Name="LastModificationAgentID" Type="Int32" />
        <asp:Parameter Name="StartDateTime" Type="DateTime" />
        <asp:Parameter Name="StopDateTime" Type="DateTime" />
        <asp:Parameter Name="ResultAction" Type="String" />
        <asp:Parameter Name="DateTimeNextAction" Type="DateTime" />
        <asp:Parameter Name="Number" Type="String" />
        <asp:Parameter Name="NewID" Type="Int32" DefaultValue="0" />
    </InsertParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceDropDownTasks" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataToClientID" TypeName="DataSet1TableAdapters.TasksTableAdapter">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="ClientID" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceDropDownContacts" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataToClientID" TypeName="DataSet1TableAdapters.ContactsTableAdapter">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="ClientID" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceActivitieType" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetData" TypeName="DataSet1TableAdapters.ActivitieTypeTableAdapter">
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceGoAgent" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetData" TypeName="DataSet1TableAdapters.AgentsTableAdapter"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceDropDownClient" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetData" TypeName="DataSet1TableAdapters.ClientsTableAdapter"></asp:ObjectDataSource>
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
����������� ��������
-->
<asp:DetailsView ID="DetailsViewActivitieDetails" runat="server" Width="100%" AutoGenerateRows="False"
    DataKeyNames="ID" DataSourceID="ObjectDataSourceActivitieDetails">
    <Fields>
        <%--  ��������� �������� ���� � ��������
        
        --%>
        <asp:TemplateField SortExpression="ClientID" HeaderText="������">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListClientID" runat="server" DataSourceID="ObjectDataSourceDropDownClient"
                    DataTextField="Name" DataValueField="ID" Visible='<%# IsAllCurrentZero  %>' SelectedValue='<%# DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Edit ? GetStatics.GetClientID(CurrentClientID, (int)Eval("TaskID"), (int)Eval("ContactID")) : GetStatics.GetFirstClientID() %>'
                    AutoPostBack="true" OnDataBound="DropDownListClientID_DataBound" OnSelectedIndexChanged="DropDownListClientID_SelectedIndexChanged"
                    Enabled="false">
                </asp:DropDownList>
                <asp:Label ID="LabelClient2ID" runat="server" Text='<%# GetStatics.GetNameClient(LinkedClient) %>'
                    Visible='<%# !IsAllCurrentZero %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelClientID" runat="server" Text='<%# GetStatics.GetNameClient(LinkedClient) %>'
                    Visible='<%# !IsAllCurrentZero %>'></asp:Label>
                <asp:Label ID="LabelClient3ID" runat="server" Text='<%# GetStatics.GetNameClient(GetStatics.GetClientID(CurrentClientID, (int)Eval("TaskID"), (int)Eval("ContactID"))) %>'
                    Visible='<%# IsAllCurrentZero %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="TaskID" HeaderText="������">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListTaskID" runat="server" DataSourceID="ObjectDataSourceDropDownTasks"
                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("TaskID") %>'
                    Visible='<%# bool.Equals(CurrentTaskID, 0) %>' />
                <asp:Label ID="LabelTask2ID" runat="server" Text='<%# GetStatics.GetNameTask(CurrentTaskID) %>'
                    Visible='<%# !bool.Equals(CurrentTaskID, 0) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelTaskID" runat="server" Text='<%# GetStatics.GetNameTask(Eval("TaskID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="ContactID" HeaderText="��������">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListContactID" runat="server" DataSourceID="ObjectDataSourceDropDownContacts"
                    DataTextField="Nik" DataValueField="ID" SelectedValue='<%# Bind("ContactID") %>'
                    Visible='<%# bool.Equals(CurrentContactID, 0) %>' />
                <asp:Label ID="LabelContact2ID" runat="server" Text='<%# GetStatics.GetNameContact(CurrentContactID) %>'
                    Visible='<%# !bool.Equals(CurrentContactID, 0) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelContactID" runat="server" Text='<%# GetStatics.GetNameContact(Eval("ContactID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Number" HeaderText="�����" SortExpression="Number" />
        <asp:BoundField DataField="Name" HeaderText="�������� ��������" SortExpression="Name" />
        <asp:TemplateField SortExpression="TypeID" HeaderText="���">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListTypeID" runat="server" DataSourceID="ObjectDataSourceActivitieType"
                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("TypeID") %>' />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelTypeID" runat="server" Text='<%# GetStatics.GetNameType(Eval("TypeID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="AgentID" HeaderText="�������� ������">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListGoAgent" runat="server" DataSourceID="ObjectDataSourceGoAgent"
                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("AgentID") %>' />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelGoAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("AgentID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CreationDateTime" HeaderText="���� ��������" InsertVisible="False"
            ReadOnly="True" SortExpression="CreationDateTime" />
        <asp:TemplateField SortExpression="CreationAgentID" HeaderText="������ �����" InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelCreateAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("CreationAgentID")) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelCreateAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("CreationAgentID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="LastModificationDateTime" HeaderText="���� ���������"
            InsertVisible="False" ReadOnly="True" SortExpression="LastModificationDateTime" />
        <asp:TemplateField SortExpression="LastModificationAgentID" HeaderText="����� ��������� ���������"
            InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelLastModifAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("LastModificationAgentID")) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelLastModifAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("LastModificationAgentID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="StartDateTime" HeaderText="������ ��������" SortExpression="StartDateTime" />
        <asp:BoundField DataField="StopDateTime" HeaderText="����� ��������" SortExpression="StopDateTime" />
        <asp:BoundField DataField="ResultAction" HeaderText="��������� ��������" SortExpression="ResultAction" />
        <asp:BoundField DataField="DateTimeNextAction" HeaderText="���� ���������� ��������"
            SortExpression="DateTimeNextAction" />
    </Fields>
    <EmptyDataTemplate>
        <br />
        <b>�� ���������� ��������</b>
        <br />
    </EmptyDataTemplate>
</asp:DetailsView>
<!-- 
�������������� ������
-->
<quli:AddonInfo ID="AddonInfo3" runat="server" RelationTable="activitie" X_ID="0" />
