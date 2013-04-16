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

public partial class Controls_Activities : System.Web.UI.UserControl
{
    /// <summary>
    /// ������ �������� ����������� ������ ������� ��������, 
    /// ���� 0 ������� ��� ��������
    /// </summary>    
    public int BindContactID
    {
        get
        {
            object obj = ViewState["CONTACT_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["CONTACT_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// ����� ������ ����������� ������ ������� ��������, 
    /// ���� 0 ������� ��� ��������
    /// </summary>    
    public int BindTaskID
    {
        get
        {
            object obj = ViewState["TASK_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["TASK_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// ������ ������� ����������� ������ ������� ��������, 
    /// ���� 0 ������� ��� ��������
    /// </summary>    
    public int BindClientID
    {
        get
        {
            object obj = ViewState["CLIENT_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["CLIENT_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// �������� �������� ��������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack) this.DataBind();
    }

    /// <summary>
    /// �������� ������ � �������� ��������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();        
        this.ActivitiesList1.BindContactID = this.BindContactID;
        this.ActivitiesList1.BindTaskID = this.BindTaskID;
        this.ActivitiesList1.BindClientID = this.BindClientID;
        this.ActivitiesList1.DataBind();

        //�������� ��������� ���������� � Label        
        string s = string.Empty;
        if (this.BindClientID != 0)
            s += " ������ = \"" + GetStatics.GetNameClient(this.BindClientID) + "\"";
        if (this.BindTaskID != 0)
            s += (s == string.Empty ? string.Empty : ",") + " ������ = \"" + GetStatics.GetNameTask(this.BindTaskID) + "\"";
        if (this.BindContactID != 0)
            s += (s == string.Empty ? string.Empty : ",") + " ������� = \"" + GetStatics.GetNameContact(this.BindContactID) + "\"";
        if (s != string.Empty)
            this.LabelActivities1.Text = "�������� ( " + s + " ) ";
    }

    /// <summary>
    /// ��������� �������, ����� �������� ��� ��������� � ������� ����������� ��������
    /// </summary>    
    protected void ActivitiesList1_ActivitiesSelected(object sender, EventArgs e)
    {
        this.ActivitieDetails1.CurrentContactID = this.BindContactID;
        this.ActivitieDetails1.CurrentTaskID = this.BindTaskID;
        this.ActivitieDetails1.CurrentClientID = this.BindClientID;
        this.ActivitieDetails1.BindActivitieID = this.ActivitiesList1.SelectedActivitieID;
        this.ActivitieDetails1.DataBind();
        this.ShowDetail();
        this.TabsTreeA1.ShowDetailTab();
    }

    /// <summary>
    /// ������������� �� ������ ��������
    /// </summary>
    public void ShowList()
    {
        this.MultiViewActivities.ActiveViewIndex = 0;
        this.TabsTreeA1.ShowListingTab();
    }

    /// <summary>
    /// ������������� �� ����������� ��������
    /// </summary>
    public void ShowDetail()
    {
        this.MultiViewActivities.ActiveViewIndex = 1;
    }

    /// <summary>
    /// ������� - ������ ������� �������� ������ ��������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeA1_ClickListing(object sender, EventArgs e)
    {
        this.ActivitieDetails1.Cancel();
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// ��������� ������� - ����� ������� ������� ��������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeA1_ClickNew(object sender, EventArgs e)
    {
        this.ActivitieDetails1.CurrentContactID = this.BindContactID;
        this.ActivitieDetails1.CurrentTaskID = this.BindTaskID;
        this.ActivitieDetails1.CurrentClientID = this.BindClientID;
        this.ActivitieDetails1.New();
        this.ShowDetail();
        this.TabsTreeA1.ShowNewTab();
    }

    /// <summary>
    /// ��������� �������, ����������� �������� �������� ������� ������������� �� ������ ��������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ActivitieDetails1_HideActivitieDetails(object sender, EventArgs e)
    {
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// ��������� ������� ����� ��������������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ActivitieDetails1_BeforeUpdate(object sender, EventArgs e)
    {
        this.TabsTreeA1.UnclickedListingTab(false);
    }

    /// <summary>
    /// ��������� ������� ����� �������������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ActivitieDetails1_AfterUpdate(object sender, EventArgs e)
    {
        this.TabsTreeA1.UnclickedListingTab(true);
    }

}
