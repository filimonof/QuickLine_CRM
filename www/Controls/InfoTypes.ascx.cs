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

public partial class Controls_InfoTypes : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.DataBind();
    }

    /// <summary>
    /// �������� ������ � �������� ���� ����������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();                
        this.InfoTypesList1.DataBind();
    }

    /// <summary>
    /// ��������� �������, ����� ���� ���������� ��� ��������� � ������� ����������� ����� ����������
    /// </summary>    
    protected void InfoTypesList1_TypeInfoSelected(object sender, EventArgs e)
    {
        this.InfoTypeDetails1.BindTypeInfoID = this.InfoTypesList1.SelectedTypeInfoID;
        this.InfoTypeDetails1.DataBind();

        this.ShowDetail();
        this.TabsTreeIT1.ShowDetailTab();

        //����� ��������� � ����� ��������������
        this.InfoTypeDetails1.Edit();
    }

    /// <summary>
    /// ������������� �� ������ ����� ����������
    /// </summary>
    public void ShowList()
    {
        this.MultiViewInfoType.ActiveViewIndex = 0;
        this.TabsTreeIT1.ShowListingTab();
    }

    /// <summary>
    /// ������������� �� ����������� ���� ����������
    /// </summary>
    public void ShowDetail()
    {
        this.MultiViewInfoType.ActiveViewIndex = 1;
    }

    /// <summary>
    /// ������� - ������ ������� �������� ������ �����
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeIT1_ClickListing(object sender, EventArgs e)
    {
        //�� ������� ���������� ����� ������ �� "������ ����� ����������"
        this.InfoTypeDetails1.Cancel();
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// ��������� ������� - ����� ������� ������� ��� ����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeIT1_ClickNew(object sender, EventArgs e)
    {
        this.InfoTypeDetails1.New();
        this.ShowDetail();
        this.TabsTreeIT1.ShowNewTab();
    }

    /// <summary>
    /// ��������� �������, ����������� ���� ���������� �������� ������� ������������� �� ������ �����
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InfoTypeDetails1_HideInfoTypeDetails(object sender, EventArgs e)
    {
        this.DataBind();
        // ���� ����� ShowList ������� ������ ��������� ����� �����������
        this.ShowList();
    }

    /// <summary>
    /// ��������� ������� ����� ��������������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InfoTypeDetails1_BeforeUpdate(object sender, EventArgs e)
    {
        this.TabsTreeIT1.UnclickedListingTab(false);
    }

    /// <summary>
    /// ��������� ������� ����� �������������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InfoTypeDetails1_AfterUpdate(object sender, EventArgs e)
    {
        this.TabsTreeIT1.UnclickedListingTab(true);
    }



}
