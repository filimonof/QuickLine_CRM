<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactDetails.ascx.cs"
    Inherits="WebUserControls_ContactDetails" %>
<%@ Register Src="AddonInfo.ascx" TagName="AddonInfo" TagPrefix="quli" %>
<%--
данные
--%>
<asp:ObjectDataSource ID="ObjectDataSourceContactDetails" runat="server" DeleteMethod="Delete"
    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataToID"
    TypeName="DataSet1TableAdapters.ContactsTableAdapter" UpdateMethod="Update" OnInserting="ObjectDataSourceContactDetails_Inserting"
    OnUpdating="ObjectDataSourceContactDetails_Updating" OnInserted="ObjectDataSourceContactDetails_Inserted">
    <DeleteParameters>
        <asp:Parameter Name="Original_ID" Type="Object" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="ClientID" Type="Int32" />
        <asp:Parameter Name="Number" Type="String" />
        <asp:Parameter Name="FirstName" Type="String" />
        <asp:Parameter Name="LastName" Type="String" />
        <asp:Parameter Name="Family" Type="String" />
        <asp:Parameter Name="Nik" Type="String" />
        <asp:Parameter Name="Sex" Type="Boolean" />
        <asp:Parameter Name="HelloWord" Type="String" />
        <asp:Parameter Name="LastModificationDateTime" Type="DateTime" />
        <asp:Parameter Name="LastModificationAgentID" Type="Int32" />
        <asp:Parameter Name="Enabled" Type="Boolean" />
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </UpdateParameters>
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="ID" Type="Int32" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Name="ClientID" Type="Int32" />
        <asp:Parameter Name="Number" Type="String" />
        <asp:Parameter Name="FirstName" Type="String" />
        <asp:Parameter Name="LastName" Type="String" />
        <asp:Parameter Name="Family" Type="String" />
        <asp:Parameter Name="Nik" Type="String" />
        <asp:Parameter Name="Sex" Type="Boolean" />
        <asp:Parameter Name="HelloWord" Type="String" />
        <asp:Parameter Name="CreationDateTime" Type="DateTime" />
        <asp:Parameter Name="CreationAgentID" Type="Int32" />
        <asp:Parameter Name="LastModificationDateTime" Type="DateTime" />
        <asp:Parameter Name="LastModificationAgentID" Type="Int32" />
        <asp:Parameter Name="Enabled" Type="Boolean" />
        <asp:Parameter Name="NewID" Type="Int32" DefaultValue="0" />
    </InsertParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceDropDownClients" runat="server" OldValuesParameterFormatString="original_{0}"
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
детализация контакта
-->
<asp:DetailsView ID="DetailsViewContact" runat="server" AutoGenerateRows="False"
    DataKeyNames="ID" DataSourceID="ObjectDataSourceContactDetails" Width="100%">
    <Fields>
        <asp:BoundField DataField="Number" HeaderText="Номер" SortExpression="Number" />
        <asp:CheckBoxField DataField="Enabled" HeaderText="Активен" SortExpression="Enabled" />
        <asp:BoundField DataField="FirstName" HeaderText="Имя" SortExpression="FirstName" />
        <asp:BoundField DataField="LastName" HeaderText="Отчество" SortExpression="LastName" />
        <asp:BoundField DataField="Family" HeaderText="Фамилия" SortExpression="Family" />
        <asp:BoundField DataField="Nik" HeaderText="Название" SortExpression="Nik" />
        <asp:TemplateField SortExpression="Sex" HeaderText="Пол">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListSex" runat="server" SelectedValue='<%# Bind("Sex") %>'>
                    <asp:ListItem Text=" мужской " Value="True" />
                    <asp:ListItem Text=" женский " Value="False" />
                </asp:DropDownList>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelSex" runat="server" Text='<%# GetStatics.BoolToSex(Eval("Sex")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="HelloWord" HeaderText="Обращение фразой" SortExpression="HelloWord" />
        <asp:TemplateField SortExpression="CreationDateTime" HeaderText="Дата создания клиента"
            InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelCreateDate" runat="server" Text='<%# Eval("CreationDateTime") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelCreateDate" runat="server" Text='<%# Eval("CreationDateTime") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="CreationAgentID" HeaderText="Создал агент" InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelCreateAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("CreationAgentID")) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelCreateAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("CreationAgentID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="LastModificationDateTime" HeaderText="Последнее изменение"
            InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelLastModifDate" runat="server" Text='<%# DateTime.Now + " (пересчитается)" %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelLastModifDate" runat="server" Text='<%# Eval("LastModificationDateTime") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="LastModificationAgentID" HeaderText="Агент создавший изменения"
            InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelLastModifAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("LastModificationAgentID")) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelLastModifAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("LastModificationAgentID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="ClientID" HeaderText="Клиент">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListClientsID" runat="server" DataSourceID="ObjectDataSourceDropDownClients"
                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("ClientID") %>'
                    Visible='<%# bool.Equals(CurrentClientID, 0) %>' />
                <asp:Label ID="LabelClient2ID" runat="server" Text='<%# GetStatics.GetNameClient(CurrentClientID) %>'
                    Visible='<%# !bool.Equals(CurrentClientID, 0) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelClientID" runat="server" Text='<%# GetStatics.GetNameClient(Eval("ClientID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Fields>
    <EmptyDataTemplate>
        <br />
        <b>Не определён контакт</b>
        <br />
    </EmptyDataTemplate>
</asp:DetailsView>
<!-- 
дополнительные данные
-->
<quli:AddonInfo ID="AddonInfo2" runat="server" RelationTable="contact" X_ID="0" />
