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

public partial class Controls_ActivitieDetails : System.Web.UI.UserControl
{

    /// <summary>
    /// � ����� �������� ������� ��������� ����������
    /// </summary>   
    public int BindActivitieID
    {
        get
        {
            object obj = ViewState["ACTIVITIE_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["ACTIVITIE_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// ������� �������, ��� �������� ����������� ������ ��������
    /// </summary>
    public int CurrentContactID
    {
        get
        {
            object obj = ViewState["CURRENT_CONTACT_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["CURRENT_CONTACT_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// ������� ������, �� ������� ����������� ������ ��������
    /// </summary>
    public int CurrentTaskID
    {
        get
        {
            object obj = ViewState["CURRENT_TASK_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["CURRENT_TASK_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// ������� ������, ��� �������� ����������� ������ ��������
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
    /// true - ���� ��� ��� Current..ID ����� 0
    /// ������ ������ �������� ������ �������� ��� ������������
    /// �������� ������� �������� �������� ����� ������� �������� ����� TaskID ContactID 
    /// </summary>
    protected bool IsAllCurrentZero
    {
        get { return this.CurrentClientID == 0 && this.CurrentTaskID == 0 && this.CurrentContactID == 0; }
    }

    /// <summary>
    /// � ������ ������� ��������� ������ ������� ��������
    /// ������ �������� ����� �����, ����� ���� ������ ���� �� current-�� ������� ����
    /// ���� LinkedClient=0 �� ������ �������� ������� ����� ������ �� ������� ��������� ����� TaskID ContactID
    /// </summary>
    protected int LinkedClient
    {
        get { return GetStatics.GetClientID(CurrentClientID, CurrentTaskID, CurrentContactID); }
    }

    /// <summary>
    /// ������ ��� ��������� ID �������� ��� ���������� ������
    /// </summary> 
    private int newActivitieID = 0;

    /// <summary>
    /// �������� �������� ����������� ��������
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// �������� ������ � �������� ����������� ��������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceActivitieDetails.SelectParameters.Clear();
        this.ObjectDataSourceActivitieDetails.SelectParameters.Add("ID", TypeCode.Int32, this.BindActivitieID.ToString());
        this.ObjectDataSourceActivitieDetails.DataBind();
        this.DetailsViewActivitieDetails.DataBind();

        this.AddonInfo3.RelationTable = TypeRelation.TR_ACTIVITIE;
        this.AddonInfo3.X_ID = this.BindActivitieID;
        this.AddonInfo3.DataBind();
        this.AddonInfo3.ShowView();

        this.FilteredTaskAndContact(this.LinkedClient);
    }

    /// <summary>
    /// ���������� ��������� ������ ��������������
    /// </summary>
    private void UpdateButton()
    {
        this.ButtonEdit.Visible = this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.ReadOnly;
        this.ButtonDel.Visible = this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.ReadOnly;

        this.ButtonPost.Visible =
            (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Edit);
        this.ButtonCancel.Visible =
            (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Edit);
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

    /// <summary>
    /// �������, ����� ��������������� ��������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceActivitieDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //������� ���� ����������� � ������� ����� (��������� ���������)
        e.InputParameters["LastModificationDateTime"] = DateTime.Now;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
    }

    /// <summary>
    /// �������, ����� ��� ��� �������� ����� ��������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param> 
    protected void ObjectDataSourceActivitieDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //������� ���� �������� � ����������� � ����� ��������� ���
        DateTime new_rec = DateTime.Now;
        e.InputParameters["CreationDateTime"] = new_rec;
        e.InputParameters["CreationAgentID"] = Session["CURRENT_AGENT"];
        e.InputParameters["LastModificationDateTime"] = new_rec;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
        if (this.CurrentContactID != 0)
            e.InputParameters["ContactID"] = this.CurrentContactID;
        if (this.CurrentTaskID != 0)
            e.InputParameters["TaskID"] = this.CurrentTaskID;
    }

    /// <summary>
    /// ������� �� ����������� �������� - ����� ID ������ ��������
    /// </summary>
    protected void ObjectDataSourceActivitieDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.newActivitieID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
    }

    /// <summary>
    /// ������ ��������
    /// </sumary>
    public void New()
    {
        if (this.BeforeInsert != null) this.BeforeInsert(this, new EventArgs());
        this.DetailsViewActivitieDetails.ChangeMode(DetailsViewMode.Insert);

        this.AddonInfo3.RelationTable = TypeRelation.TR_ACTIVITIE;
        this.AddonInfo3.New();

        if (!this.IsAllCurrentZero)
            this.FilteredTaskAndContact(this.LinkedClient);
    }

    /// <summary>
    /// ������������� ������� ��������
    /// </summary>
    public void Edit()
    {
        if (this.BeforeUpdate != null) this.BeforeUpdate(this, new EventArgs());
        this.DetailsViewActivitieDetails.ChangeMode(DetailsViewMode.Edit);
        this.AddonInfo3.Edit();

        if (!this.IsAllCurrentZero)
            this.FilteredTaskAndContact(this.LinkedClient);
    }

    #region ���������� �������

    /// <summary>
    /// ������� - ����� �� �����������
    /// </summary>
    public event EventHandler HideActivitieDetails;

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
    /// ������� ������� ��������
    /// </summary>
    public void Del()
    {
        this.DetailsViewActivitieDetails.DeleteItem();
        this.AddonInfo3.Del();
        if (this.HideActivitieDetails != null) this.HideActivitieDetails(this, new EventArgs());
    }

    /// <summary>
    /// �������� �������������� ��� �������� ��������
    /// </summary>
    public void Cancel()
    {
        bool inserted = this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Insert;
        this.DetailsViewActivitieDetails.ChangeMode(DetailsViewMode.ReadOnly);
        this.AddonInfo3.Cancel();

        if (inserted)
        {
            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
            if (this.HideActivitieDetails != null) this.HideActivitieDetails(this, new EventArgs());
        }
        else
        {
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// ��������� ��������� ��� ����� ��������
    /// </summary>
    public void Post()
    {
        if (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Insert)
        {
            this.DetailsViewActivitieDetails.InsertItem(true);
            // ��� ���������� ������� OnInserted � newActivitieID ������� ��� ��������            
            this.AddonInfo3.Post(this.newActivitieID);
            this.newActivitieID = 0;

            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
            if (this.HideActivitieDetails != null) this.HideActivitieDetails(this, new EventArgs());
        }
        else if (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Edit)
        {
            this.DetailsViewActivitieDetails.UpdateItem(true);
            this.AddonInfo3.Post(0);
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// ����������� �������� � ������ ������������� �������� �������
    /// </summary>
    /// <param name="clientID">������</param>
    protected void FilteredTaskAndContact(int clientID)
    {
        this.ObjectDataSourceDropDownTasks.SelectParameters.Clear();
        this.ObjectDataSourceDropDownTasks.SelectParameters.Add("ClientID", TypeCode.Int32, clientID.ToString());
        this.ObjectDataSourceDropDownTasks.DataBind();

        this.ObjectDataSourceDropDownContacts.SelectParameters.Clear();
        this.ObjectDataSourceDropDownContacts.SelectParameters.Add("ClientID", TypeCode.Int32, clientID.ToString());
        this.ObjectDataSourceDropDownContacts.DataBind();
    }

    /// <summary>
    /// ������� �� �������� ������ � ��������� ��������
    /// </summary>
    protected void DropDownListClientID_DataBound(object sender, EventArgs e)
    {
        this.DropDownListClientID_SelectedIndexChanged(sender, e);
    }

    /// <summary>
    /// ������� �� ��������� ��������� ��������
    /// </summary>
    protected void DropDownListClientID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.IsAllCurrentZero)
        {
            //int clientID;
            //string strClientID = ((DropDownList)sender).SelectedValue;
            //if (int.TryParse(strClientID, out clientID))
            //    this.FilteredTaskAndContact(clientID);
        }
    }

}
