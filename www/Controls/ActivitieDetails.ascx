<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActivitieDetails.ascx.cs"
    Inherits="Controls_ActivitieDetails" %>
<%@ Register Src="AddonInfo.ascx" TagName="AddonInfo" TagPrefix="quli" %>
<%--
данные
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
Кнопки
-->
<asp:Button ID="ButtonEdit" runat="server" Text="Редактировать" OnClick="ButtonEdit_Click"
    OnPreRender="Button_PreRender" />
<asp:Button ID="ButtonDel" runat="server" Text="Удалить" OnClick="ButtonDel_Click"
    OnPreRender="Button_PreRender" />
<asp:Button ID="ButtonPost" runat="server" Text="Сохранить" OnClick="ButtonPost_Click"
    OnPreRender="Button_PreRender" />
<asp:Button ID="ButtonCancel" runat="server" Text="Отменить" OnClick="ButtonCancel_Click"
    OnPreRender="Button_PreRender" />
<br />
<!-- 
детализация действия
-->
<asp:DetailsView ID="DetailsViewActivitieDetails" runat="server" Width="100%" AutoGenerateRows="False"
    DataKeyNames="ID" DataSourceID="ObjectDataSourceActivitieDetails">
    <Fields>
        <%--  попробуем добавить поле с клиентом
        
        --%>
        <asp:TemplateField SortExpression="ClientID" HeaderText="Клиент">
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
        <asp:TemplateField SortExpression="TaskID" HeaderText="Задачи">
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
        <asp:TemplateField SortExpression="ContactID" HeaderText="Контакты">
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
        <asp:BoundField DataField="Number" HeaderText="Номер" SortExpression="Number" />
        <asp:BoundField DataField="Name" HeaderText="Название действия" SortExpression="Name" />
        <asp:TemplateField SortExpression="TypeID" HeaderText="Тип">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListTypeID" runat="server" DataSourceID="ObjectDataSourceActivitieType"
                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("TypeID") %>' />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelTypeID" runat="server" Text='<%# GetStatics.GetNameType(Eval("TypeID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="AgentID" HeaderText="Поручена агенту">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListGoAgent" runat="server" DataSourceID="ObjectDataSourceGoAgent"
                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("AgentID") %>' />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelGoAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("AgentID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CreationDateTime" HeaderText="Дата создания" InsertVisible="False"
            ReadOnly="True" SortExpression="CreationDateTime" />
        <asp:TemplateField SortExpression="CreationAgentID" HeaderText="Создал агент" InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelCreateAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("CreationAgentID")) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelCreateAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("CreationAgentID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="LastModificationDateTime" HeaderText="Дата изменения"
            InsertVisible="False" ReadOnly="True" SortExpression="LastModificationDateTime" />
        <asp:TemplateField SortExpression="LastModificationAgentID" HeaderText="Агент создавший изменения"
            InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelLastModifAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("LastModificationAgentID")) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelLastModifAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("LastModificationAgentID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="StartDateTime" HeaderText="Начало действия" SortExpression="StartDateTime" />
        <asp:BoundField DataField="StopDateTime" HeaderText="Конец действия" SortExpression="StopDateTime" />
        <asp:BoundField DataField="ResultAction" HeaderText="Результат действия" SortExpression="ResultAction" />
        <asp:BoundField DataField="DateTimeNextAction" HeaderText="Дата следующего действия"
            SortExpression="DateTimeNextAction" />
    </Fields>
    <EmptyDataTemplate>
        <br />
        <b>Не определено действие</b>
        <br />
    </EmptyDataTemplate>
</asp:DetailsView>
<!-- 
дополнительные данные
-->
<quli:AddonInfo ID="AddonInfo3" runat="server" RelationTable="activitie" X_ID="0" />
