<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaskDetails.ascx.cs" Inherits="Controls_TaskDetails" %>
<%@ Register Src="AddonInfo.ascx" TagName="AddonInfo" TagPrefix="quli" %>
<%--
данные
--%>
<asp:ObjectDataSource ID="ObjectDataSourceTaskDetails" runat="server" DeleteMethod="Delete"
    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" OnInserting="ObjectDataSourceTaskDetails_Inserting"
    OnUpdating="ObjectDataSourceTaskDetails_Updating" SelectMethod="GetDataToID"
    TypeName="DataSet1TableAdapters.TasksTableAdapter" UpdateMethod="Update" OnInserted="ObjectDataSourceTaskDetails_Inserted">
    <DeleteParameters>
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="ClientID" Type="Int32" />
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="Number" Type="String" />
        <asp:Parameter Name="CategoryID" Type="Int32" />
        <asp:Parameter Name="Status" Type="Int32" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="LastModificationDateTime" Type="DateTime" />
        <asp:Parameter Name="LastModificationAgentID" Type="Int32" />
        <asp:Parameter Name="Original_ID" Type="Int32" />
    </UpdateParameters>
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="ID" Type="Int32" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Name="ClientID" Type="Int32" />
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="Number" Type="String" />
        <asp:Parameter Name="CategoryID" Type="Int32" />
        <asp:Parameter Name="Status" Type="Int32" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="CreationDateTime" Type="DateTime" />
        <asp:Parameter Name="CreationAgentID" Type="Int32" />
        <asp:Parameter Name="LastModificationDateTime" Type="DateTime" />
        <asp:Parameter Name="LastModificationAgentID" Type="Int32" />
        <asp:Parameter Name="NewID" Type="Int32" DefaultValue="0" />
    </InsertParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceDropDownClients" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetData" TypeName="DataSet1TableAdapters.ClientsTableAdapter"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceCategory" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetData" TypeName="DataSet1TableAdapters.CategoryTableAdapter"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSourceStatus" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="List" TypeName="TypeStatus"></asp:ObjectDataSource>
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
детализация задачи
-->
<asp:DetailsView ID="DetailsViewTask" runat="server" AutoGenerateRows="False" DataKeyNames="ID"
    DataSourceID="ObjectDataSourceTaskDetails" Width="100%">
    <Fields>
        <asp:BoundField DataField="Number" HeaderText="Номер" SortExpression="Number" />
        <asp:BoundField DataField="Name" HeaderText="Название" SortExpression="Name" />
        <asp:TemplateField SortExpression="Status" HeaderText="Статус">
            <ItemTemplate>
                <asp:Label ID="LabelStatus" runat="server" Text='<%# TypeStatus.StatusToString(Eval("Status")) %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListStatus" runat="server" SelectedValue='<%# Bind("Status") %>'>
                    <asp:ListItem Text=" open " Value="0" />
                    <asp:ListItem Text=" close " Value="1" />
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="CategoryID" HeaderText="Категория">
            <EditItemTemplate>
                <asp:DropDownList ID="DropDownListCategortID" runat="server" DataSourceID="ObjectDataSourceCategory"
                    DataTextField="Name" DataValueField="ID" SelectedValue='<%# Bind("CategoryID") %>' />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelCategoryID" runat="server" Text='<%# GetStatics.GetNameCategory(Eval("CategoryID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Description" HeaderText="Описание" SortExpression="Description" />
        <asp:BoundField DataField="CreationDateTime" HeaderText="Дата создания" SortExpression="CreationDateTime"
            InsertVisible="False" ReadOnly="True" />
        <asp:TemplateField SortExpression="CreationAgentID" HeaderText="Создал агент" InsertVisible="False">
            <EditItemTemplate>
                <asp:Label ID="LabelCreateAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("CreationAgentID")) %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelCreateAgent" runat="server" Text='<%# GetStatics.GetNameAgent(Eval("CreationAgentID")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="LastModificationDateTime" HeaderText="Дата изменения"
            SortExpression="LastModificationDateTime" InsertVisible="False" ReadOnly="True" />
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
        <b>Задача не определена</b>
        <br />
    </EmptyDataTemplate>
</asp:DetailsView>
<!-- 
дополнительные данные
-->
<quli:AddonInfo ID="AddonInfo4" runat="server" RelationTable="task" X_ID="0" />
