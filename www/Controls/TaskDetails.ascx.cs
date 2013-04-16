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

public partial class Controls_TaskDetails : System.Web.UI.UserControl
{
    /// <summary>
    /// � ����� ������ ������� ��������� ����������
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
    /// ������� ������, ��� �������� ����������� ������ ������
    /// </summary>
    public int CurrentClientID
    {
        get
        {
            object obj = ViewState["CURRENT_CLIENT_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["CURRENT_CLIENT_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// ������ ��� ��������� ID ������ ��� ���������� ������
    /// </summary> 
    private int newTaskID = 0;

    /// <summary>
    /// �������� �������� ����������� ������
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// �������� ������ � �������� ����������� ������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceTaskDetails.SelectParameters.Clear();
        this.ObjectDataSourceTaskDetails.SelectParameters.Add("ID", TypeCode.Int32, this.BindTaskID.ToString());
        this.ObjectDataSourceTaskDetails.DataBind();
        this.DetailsViewTask.DataBind();

        this.AddonInfo4.RelationTable = TypeRelation.TR_TASK;
        this.AddonInfo4.X_ID = this.BindTaskID;
        this.AddonInfo4.DataBind();
        this.AddonInfo4.ShowView();
    }

    /// <summary>
    /// ���������� ��������� ������ ��������������
    /// </summary>
    private void UpdateButton()
    {
        this.ButtonEdit.Visible = this.DetailsViewTask.CurrentMode == DetailsViewMode.ReadOnly;
        this.ButtonDel.Visible = this.DetailsViewTask.CurrentMode == DetailsViewMode.ReadOnly;

        this.ButtonPost.Visible =
            (this.DetailsViewTask.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewTask.CurrentMode == DetailsViewMode.Edit);
        this.ButtonCancel.Visible =
            (this.DetailsViewTask.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewTask.CurrentMode == DetailsViewMode.Edit);
    }

    /// <summary>
    /// ����� ������
    /// </sumary>
    public void New()
    {
        if (this.BeforeInsert != null) this.BeforeInsert(this, new EventArgs());
        this.DetailsViewTask.ChangeMode(DetailsViewMode.Insert);

        this.AddonInfo4.RelationTable = TypeRelation.TR_TASK;
        this.AddonInfo4.New();
    }

    /// <summary>
    /// ������������� ������� ������
    /// </summary>
    public void Edit()
    {
        if (this.BeforeUpdate != null) this.BeforeUpdate(this, new EventArgs());
        this.DetailsViewTask.ChangeMode(DetailsViewMode.Edit);
        this.AddonInfo4.Edit();
    }

    #region ���������� �������

    /// <summary>
    /// ������� - ����� �� �����������
    /// </summary>
    public event EventHandler HideTaskDetails;

    /// <summary>
    /// ������� - ����� �����������
    /// </summary>
    public event EventHandler BeforeInsert;
    /// <summary>
    /// ������� - ����� ����������
    /// </summary>
    public event EventHandler AfterInsert;

    /// <summary>
    /// ������� - ����� ���������������
    /// </summary>
    public event EventHandler BeforeUpdate;
    /// <summary>
    /// ������� - ����� ���������������
    /// </summary>
    public event EventHandler AfterUpdate;

    #endregion

    /// <summary>
    /// ������� ������� ������
    /// </summary>
    public void Del()
    {
        this.DetailsViewTask.DeleteItem();
        this.AddonInfo4.Del();
        if (this.HideTaskDetails != null) this.HideTaskDetails(this, new EventArgs());
    }

    /// <summary>
    /// �������� �������������� ��� �������� ������
    /// </summary>
    public void Cancel()
    {
        bool inserted = this.DetailsViewTask.CurrentMode == DetailsViewMode.Insert;
        this.DetailsViewTask.ChangeMode(DetailsViewMode.ReadOnly);
        this.AddonInfo4.Cancel();

        if (inserted)
        {
            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
            if (this.HideTaskDetails != null) this.HideTaskDetails(this, new EventArgs());
        }
        else
        {
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// ��������� ��������� ��� ����� ������
    /// </summary>
    public void Post()
    {
        if (this.DetailsViewTask.CurrentMode == DetailsViewMode.Insert)
        {
            this.DetailsViewTask.InsertItem(true);
            // ��� ���������� ������� OnInserted � newTaskID ������� ��� ��������            
            this.AddonInfo4.Post(this.newTaskID);
            this.newTaskID = 0;

            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
            if (this.HideTaskDetails != null) this.HideTaskDetails(this, new EventArgs());
        }
        else if (this.DetailsViewTask.CurrentMode == DetailsViewMode.Edit)
        {
            this.DetailsViewTask.UpdateItem(true);
            this.AddonInfo4.Post(0);
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// �������, ����� ��� ��� ������� ����� ������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceTaskDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //������� ���� �������� � ����������� � ����� ��������� ���
        DateTime new_rec = DateTime.Now;
        e.InputParameters["CreationDateTime"] = new_rec;
        e.InputParameters["CreationAgentID"] = Session["CURRENT_AGENT"];
        e.InputParameters["LastModificationDateTime"] = new_rec;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
        if (this.CurrentClientID != 0)
            e.InputParameters["ClientID"] = this.CurrentClientID;
    }

    /// <summary>
    /// �������, ����� ��������������� �����
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceTaskDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //������� ���� ����������� � ������� ����� (��������� ���������)
        e.InputParameters["LastModificationDateTime"] = DateTime.Now;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
    }

    /// <summary>
    /// ������� �� ����������� ������ - ����� ID ����� ������
    /// </summary>
    protected void ObjectDataSourceTaskDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.newTaskID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
    }

    #region ������� ������

    /// <summary>
    /// ������ ������ ������������� 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        this.Edit();
    }

    /// <summary>
    /// ������ ������ ������ 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        this.Cancel();
    }

    /// <summary>
    /// ������ ������ ������� 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonDel_Click(object sender, EventArgs e)
    {
        this.Del();
    }

    /// <summary>
    /// ������ ������ ��������� 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonPost_Click(object sender, EventArgs e)
    {
        this.Post();
    }

    #endregion

    /// <summary>
    /// ������� ����� ����������� ������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_PreRender(object sender, EventArgs e)
    {
        this.UpdateButton();
    }

}
