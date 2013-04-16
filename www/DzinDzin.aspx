<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DzinDzin.aspx.cs" Inherits="DzinDzin" %>

<%@ Register Src="Controls/AddonInfo.ascx" TagName="AddonInfo" TagPrefix="quli" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderDesktop" runat="Server">
    <!-- ��������� -->
    <asp:ObjectDataSource ID="ObjectDataSourceActivitieTypeDz" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="DataSet1TableAdapters.ActivitieTypeTableAdapter">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceDropDownClients" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="DataSet1TableAdapters.TasksTableAdapter"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceDropDownTasks" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataToClientID" TypeName="DataSet1TableAdapters.TasksTableAdapter">
        <selectparameters>
        <asp:Parameter DefaultValue="0" Name="ClientID" Type="Int32" />
    </selectparameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceDropDownContacts" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataToClientID" TypeName="DataSet1TableAdapters.ContactsTableAdapter">
        <selectparameters>
        <asp:Parameter DefaultValue="0" Name="ClientID" Type="Int32" />
    </selectparameters>
    </asp:ObjectDataSource>
    <!-- ����� ���� ������ -->
    <table border="0" cellpadding="3" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 60%">
                <table border="0" cellpadding="3" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="width: 250px">
                            <asp:Literal ID="LiteralTel1" runat="server" Text="����� ��������: " />
                        </td>
                        <td>
                            <%-- ����� ������ --%>
                            <asp:Label ID="LabelTel" runat="server" Text="?" Font-Bold="true" Font-Size="Large" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px">
                            <asp:Literal ID="LiteralTel2" runat="server" Text="�������������� ��������: " />
                        </td>
                        <td>
                            <%-- ��� ������ --%>
                            <asp:Literal ID="LiteralTelMsg" runat="server" Text="�� ���������� " />
                            <asp:DropDownList ID="DropDownListTel" runat="server" Visible="false" DataTextField="Name"
                                DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="DropDownListTel_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <%--  --%>
                    <tr>
                        <td style="width: 250px">
                            <asp:Literal ID="LiteralMode1" runat="server" Text="��� ����������: " />
                        </td>
                        <td>
                            <%-- ��� ������  --%>
                            <asp:DropDownList ID="DropDownListMode" runat="server" DataSourceID="ObjectDataSourceActivitieTypeDz"
                                DataTextField="Name" DataValueField="ID" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px">
                            <asp:Literal ID="LiteralNumber" runat="server" Text="�����: " />
                        </td>
                        <td>
                            <%-- ����� ��������  --%>
                            <asp:TextBox ID="TextBoxNumber" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px">
                            <asp:Literal ID="LiteralName" runat="server" Text="��������: " />
                        </td>
                        <td>
                            <%-- ��� ��������  --%>
                            <asp:TextBox ID="TextBoxName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px">
                            <asp:Literal ID="LiteralResultAction" runat="server" Text="��������� ��������: " />
                        </td>
                        <td>
                            <%-- ��������� ��������  --%>
                            <asp:TextBox ID="TextBoxResultAction" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px">
                            <asp:Literal ID="LiteralDateTimeNextAction" runat="server" Text="���� ���������� ��������: " />
                        </td>
                        <td>
                            <%-- ���� ���������� ��������  --%>
                            <asp:TextBox ID="TextBoxDateTimeNextAction" runat="server" />
                        </td>
                    </tr>
                </table>
                <%-- �������������� ������ --%>
                <quli:AddonInfo ID="AddonInfoDz" runat="server" RelationTable="activitie" X_ID="0" />
            </td>
            <td valign="top" style="padding: 20px 5 px 20px 5 px">
                <asp:Literal ID="LiteralClient" runat="server" Text="������: " />
                <br />
                <asp:Label ID="LabelClient" runat="server" Text="?" />
                <asp:DropDownList ID="DropDownListClient" runat="server" DataSourceID="ObjectDataSourceDropDownClients"
                    DataTextField="Name" DataValueField="ID" Visible="false" />
                <br />
                <br />
                <asp:Literal ID="LiteralContact" runat="server" Text="�������: " />
                <br />
                <asp:Label ID="LabelContact" runat="server" Text="?" />
                <asp:DropDownList ID="DropDownListContact" runat="server" DataSourceID="ObjectDataSourceDropDownContacts"
                    DataTextField="Nik" DataValueField="ID" Visible="false" />
                <br />
                <br />
                <asp:Literal ID="LiteralTask" runat="server" Text="������: " />
                <br />
                <asp:Label ID="LabelTask" runat="server" Text="?" />
                <asp:DropDownList ID="DropDownListTask" runat="server" DataSourceID="ObjectDataSourceDropDownTasks"
                    DataTextField="Name" DataValueField="ID" Visible="false" />
                <br />
                <br />
            </td>
        </tr>
    </table>
    <asp:HiddenField id="HiddenFieldTask" runat="server" Value="0" />
    <asp:HiddenField id="HiddenFieldContact" runat="server" Value="0" />
    <asp:HiddenField id="HiddenFieldStartDateTime" runat="server" />
    <br />
    <br />
    <asp:Button ID="ButtonSaveDzin" runat="server" Text="���������" OnClick="ButtonSaveDzin_Click" OnPreRender="ButtonSaveDzin_PreRender" />
    <%-- 
    �������������� ����
        CreationDateTime
        CreationAgent
        LastModificationDateTime
        LastModificationAgentID
        StartDateTime
        StopDateTime
        AgentID  = null
    ���� ������
        TypeID       
        Name
        ResultAction
        DateTimeNextAction
        Number
        �������������� ����� 
    ������� �����
        TaskID
        ContactID
        ClienID (�� ������)                 
    --%>
</asp:Content>
