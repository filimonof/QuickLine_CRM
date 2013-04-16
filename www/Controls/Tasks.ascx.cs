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

public partial class Controls_Tasks : System.Web.UI.UserControl
{

    /// <summary>
    /// ������ ������� ����������� ������ ������� ������, 
    /// ���� 0 ������� ��� ������
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
    /// �������� �������� ������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.DataBind();
    }

    /// <summary>
    /// �������� ������ � �������� ������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();       
        this.TasksList1.BindClientID = this.BindClientID;
        this.TasksList1.DataBind();

        //�������� ��������� ���������� � Label        
        string s = string.Empty;
        if (this.BindClientID != 0)
            s += " ������ = \"" + GetStatics.GetNameClient(this.BindClientID) + "\"";
        if (s != string.Empty)
            this.LabelTasks1.Text = "������ ( " + s + " ) ";
    }

    /// <summary>
    /// ��������� �������, ����� �������� ��� ��������� � ������� ����������� ������
    /// </summary>    
    protected void TasksList1_TaskSelected(object sender, EventArgs e)
    {
        this.TaskDetails1.CurrentClientID = this.BindClientID;
        this.TaskDetails1.BindTaskID = this.TasksList1.SelectedTaskID;
        this.TaskDetails1.DataBind();

        //������� �������� ��������� ������
        this.Activities2.BindContactID = 0;
        this.Activities2.BindTaskID = this.TasksList1.SelectedTaskID;
        this.Activities2.BindClientID = 0;
        this.Activities2.DataBind();
        this.Activities2.ShowList();

        this.ShowDetail();
        this.TabsTreeT1.ShowDetailTab();
    }

    /// <summary>
    /// ������������� �� ������ �����
    /// </summary>
    public void ShowList()
    {
        this.MultiViewTasks.ActiveViewIndex = 0;
        this.TabsTreeT1.ShowListingTab();
    }

    /// <summary>
    /// ������������� �� ����������� ������
    /// </summary>
    public void ShowDetail()
    {
        this.MultiViewTasks.ActiveViewIndex = 1;
    }

    /// <summary>
    /// ������� - ������ ������� �������� ������ �����
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeT1_ClickListing(object sender, EventArgs e)
    {
        this.TaskDetails1.Cancel();
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// ��������� ������� - ����� ������� ������� ������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeT1_ClickNew(object sender, EventArgs e)
    {
        this.TaskDetails1.CurrentClientID = this.BindClientID;
        this.TaskDetails1.New();
        this.ShowDetail();
        this.TabsTreeT1.ShowNewTab();
    }

    /// <summary>
    /// ��������� �������, ����������� ������ �������� ������� ������������� �� ������ �����
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskDetails1_HideTaskDetails(object sender, EventArgs e)
    {
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// ��������� ������� ����� ����������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskDetails1_BeforeInsert(object sender, EventArgs e)
    {
        this.Activities2.Visible = false;
    }

    /// <summary>
    /// ��������� ������� ����� ���������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskDetails1_AfterInsert(object sender, EventArgs e)
    {
        this.Activities2.Visible = true;
    }

    /// <summary>
    /// ��������� ������� ����� ��������������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskDetails1_BeforeUpdate(object sender, EventArgs e)
    {
        this.TabsTreeT1.UnclickedListingTab(false);
    }

    /// <summary>
    /// ��������� ������� ����� �������������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskDetails1_AfterUpdate(object sender, EventArgs e)
    {
        this.TabsTreeT1.UnclickedListingTab(true);
    }

}
