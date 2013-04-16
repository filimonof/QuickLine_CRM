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

public partial class WebUserControls_ContactDetails : System.Web.UI.UserControl
{
    /// <summary>
    /// � ����� �������� ������� ��������� ����������
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
    /// ������� ������, ��� �������� ����������� ������ �������
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
    /// ������ ��� ��������� ID �������� ��� ���������� ������
    /// </summary> 
    private int newContactID = 0;

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
        this.ObjectDataSourceContactDetails.SelectParameters.Clear();
        this.ObjectDataSourceContactDetails.SelectParameters.Add("ID", TypeCode.Int32, this.BindContactID.ToString());
        this.ObjectDataSourceContactDetails.DataBind();
        this.DetailsViewContact.DataBind();

        this.AddonInfo2.RelationTable = TypeRelation.TR_CONTACT;
        this.AddonInfo2.X_ID = this.BindContactID;
        this.AddonInfo2.DataBind();
        this.AddonInfo2.ShowView();
    }

    /// <summary>
    /// ���������� ��������� ������ ��������������
    /// </summary>
    private void UpdateButton()
    {
        this.ButtonEdit.Visible = this.DetailsViewContact.CurrentMode == DetailsViewMode.ReadOnly;
        this.ButtonDel.Visible = this.DetailsViewContact.CurrentMode == DetailsViewMode.ReadOnly;

        this.ButtonPost.Visible =
            (this.DetailsViewContact.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewContact.CurrentMode == DetailsViewMode.Edit);
        this.ButtonCancel.Visible =
            (this.DetailsViewContact.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewContact.CurrentMode == DetailsViewMode.Edit);
    }

    /// <summary>
    /// ������ �������
    /// </sumary>
    public void New()
    {
        if (this.BeforeInsert != null) this.BeforeInsert(this, new EventArgs());
        this.DetailsViewContact.ChangeMode(DetailsViewMode.Insert);

        this.AddonInfo2.RelationTable = TypeRelation.TR_CONTACT;
        this.AddonInfo2.New();
    }

    /// <summary>
    /// ������������� ������� �������
    /// </summary>
    public void Edit()
    {
        if (this.BeforeUpdate != null) this.BeforeUpdate(this, new EventArgs());
        this.DetailsViewContact.ChangeMode(DetailsViewMode.Edit);
        this.AddonInfo2.Edit();
    }

    #region ���������� �������

    /// <summary>
    /// ������� - ����� �� �����������
    /// </summary>
    public event EventHandler HideContactDetails;

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
    /// ������� ������� �������
    /// </summary>
    public void Del()
    {
        this.DetailsViewContact.DeleteItem();
        this.AddonInfo2.Del();
        if (this.HideContactDetails != null) this.HideContactDetails(this, new EventArgs());
    }

    /// <summary>
    /// �������� �������������� ��� �������� ��������
    /// </summary>
    public void Cancel()
    {
        bool inserted = this.DetailsViewContact.CurrentMode == DetailsViewMode.Insert;
        this.DetailsViewContact.ChangeMode(DetailsViewMode.ReadOnly);
        this.AddonInfo2.Cancel();

        if (inserted)
        {
            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
            if (this.HideContactDetails != null) this.HideContactDetails(this, new EventArgs());
        }
        else
        {
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// ��������� ��������� ��� ������ ��������
    /// </summary>
    public void Post()
    {
        if (this.DetailsViewContact.CurrentMode == DetailsViewMode.Insert)
        {
            this.DetailsViewContact.InsertItem(true);
            // ��� ���������� ������� OnInserted � newContactID ������� ��� ��������            
            this.AddonInfo2.Post(this.newContactID);
            this.newContactID = 0;

            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
            if (this.HideContactDetails != null) this.HideContactDetails(this, new EventArgs());
        }
        else if (this.DetailsViewContact.CurrentMode == DetailsViewMode.Edit)
        {
            this.DetailsViewContact.UpdateItem(true);
            this.AddonInfo2.Post(0);
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// �������, ����� ��� ��� ������� ����� �������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceContactDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
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
    /// �������, ����� �������������� ��������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceContactDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //������� ���� ����������� � ������� ����� (��������� ���������)
        e.InputParameters["LastModificationDateTime"] = DateTime.Now;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
    }

    /// <summary>
    /// ������� �� ����������� ������� - ����� ID ������ �������
    /// </summary>
    protected void ObjectDataSourceContactDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.newContactID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
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


