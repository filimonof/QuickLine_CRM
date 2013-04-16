using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Controls_Clients : System.Web.UI.UserControl
{
    /// <summary>
    /// �������� ��������
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // ��� ������ ���� ���� �������
        if (!IsPostBack) this.DataBind();
    }

    /// <summary>
    /// �������� ������ � �������� �������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();                
        this.ClientsList1.DataBind();
    }

    /// <summary>
    /// ��������� �������, ����� ������� ��� ��������� � ������� ����������� ��������
    /// </summary>    
    protected void ClientsList_ClientSelected(object sender, EventArgs e)
    {
        this.ClientDetails1.BindClientID = this.ClientsList1.SelectedClientID;
        this.ClientDetails1.DataBind();

        //������� �������� ���������� �������
        this.Contacts1.BindClientID = this.ClientsList1.SelectedClientID;
        this.Contacts1.DataBind();
        this.Contacts1.ShowList();

        //������� ������ ���������� �������
        this.Tasks1.BindClientID = this.ClientsList1.SelectedClientID;
        this.Tasks1.DataBind();
        this.Tasks1.ShowList();

        //������� �������� ���������� �������
        this.Activities3.BindContactID = 0;
        this.Activities3.BindTaskID = 0;
        this.Activities3.BindClientID = this.ClientsList1.SelectedClientID; ;
        this.Activities3.DataBind();
        this.Activities3.ShowList();

        this.ShowDetail();
        this.TabsTreeC1.ShowDetailTab();
    }

    /// <summary>
    /// ������������� �� ������ ��������
    /// </summary>
    public void ShowList()
    {
        this.MultiViewClients.ActiveViewIndex = 0;
        this.TabsTreeC1.ShowListingTab();
    }

    /// <summary>
    /// ������������� �� ����������� ��������
    /// </summary>
    public void ShowDetail()
    {
        this.MultiViewClients.ActiveViewIndex = 1;
    }

    /// <summary>
    /// ������� - ������ ������� �������� ������ ��������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeC1_ClickListing(object sender, EventArgs e)
    {
        this.ClientDetails1.Cancel();
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// ��������� ������� - ����� ������� ������� �������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeC1_ClickNew(object sender, EventArgs e)
    {
        this.ClientDetails1.New();
        this.ShowDetail();
        this.TabsTreeC1.ShowNewTab();
    }

    /// <summary>
    /// ��������� �������, ����������� �������� �������� ������� ������������� �� ������ ���������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ClientDetails1_HideClientDetails(object sender, EventArgs e)
    {
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// ��������� ������� ����� ����������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ClientDetails1_BeforeInsert(object sender, EventArgs e)
    {
        this.Contacts1.Visible = false;
        this.Tasks1.Visible = false;
        this.Activities3.Visible = false;
    }

    /// <summary>
    /// ��������� ������� ����� ���������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ClientDetails1_AfterInsert(object sender, EventArgs e)
    {
        this.Contacts1.Visible = true;
        this.Tasks1.Visible = true;
        this.Activities3.Visible = true;
    }

    /// <summary>
    /// ��������� ������� ����� ��������������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ClientDetails1_BeforeUpdate(object sender, EventArgs e)
    {
        this.TabsTreeC1.UnclickedListingTab(false);
    }

    /// <summary>
    /// ��������� ������� ����� �������������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ClientDetails1_AfterUpdate(object sender, EventArgs e)
    {
        this.TabsTreeC1.UnclickedListingTab(true);
    }

}
