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

public partial class Controls_ClientDetails : System.Web.UI.UserControl
{
    /// <summary>
    /// � ����� ������� ������� ��������� ����������
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
    /// ������ ��� ��������� ID ������� ��� ���������� ������
    /// </summary> 
    private int newClientID = 0;

    /// <summary>
    /// �������� �������� ����������� �������
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// �������� ������ � �������� ����������� �������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceClientDetails.SelectParameters.Clear();
        this.ObjectDataSourceClientDetails.SelectParameters.Add("ID", TypeCode.Int32, this.BindClientID.ToString());
        this.ObjectDataSourceClientDetails.DataBind();
        this.DetailsViewClient.DataBind();

        this.AddonInfo1.RelationTable = TypeRelation.TR_CLIENT;
        this.AddonInfo1.X_ID = this.BindClientID;
        this.AddonInfo1.DataBind();
        this.AddonInfo1.ShowView();
    }

    /// <summary>
    /// ���������� ��������� ������ ��������������
    /// </summary>
    private void UpdateButton()
    {
        this.ButtonEdit.Visible = this.DetailsViewClient.CurrentMode == DetailsViewMode.ReadOnly;
        this.ButtonDel.Visible = this.DetailsViewClient.CurrentMode == DetailsViewMode.ReadOnly;

        this.ButtonPost.Visible =
            (this.DetailsViewClient.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewClient.CurrentMode == DetailsViewMode.Edit);
        this.ButtonCancel.Visible =
            (this.DetailsViewClient.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewClient.CurrentMode == DetailsViewMode.Edit);
    }

    /// <summary>
    /// ������ ������
    /// </sumary>
    public void New()
    {
        if (this.BeforeInsert != null) this.BeforeInsert(this, new EventArgs());
        this.DetailsViewClient.ChangeMode(DetailsViewMode.Insert);

        this.AddonInfo1.RelationTable = TypeRelation.TR_CLIENT;
        this.AddonInfo1.New();
    }

    /// <summary>
    /// ������������� �������� �������
    /// </summary>
    public void Edit()
    {
        if (this.BeforeUpdate != null) this.BeforeUpdate(this, new EventArgs());
        this.DetailsViewClient.ChangeMode(DetailsViewMode.Edit);
        this.AddonInfo1.Edit();
    }

    #region ���������� �������

    /// <summary>
    /// ������� - ����� �� �����������
    /// </summary>
    public event EventHandler HideClientDetails;

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
    /// ������� �������� �������
    /// </summary>
    public void Del()
    {        
        this.DetailsViewClient.DeleteItem();
        this.AddonInfo1.Del();
        if (this.HideClientDetails != null) this.HideClientDetails(this, new EventArgs());
    }

    /// <summary>
    /// �������� �������������� ��� �������� �������
    /// </summary>
    public void Cancel()
    {
        bool inserted = this.DetailsViewClient.CurrentMode == DetailsViewMode.Insert;
        this.DetailsViewClient.ChangeMode(DetailsViewMode.ReadOnly);
        this.AddonInfo1.Cancel();

        if (inserted)
        {
            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
            if (this.HideClientDetails != null) this.HideClientDetails(this, new EventArgs());
        }
        else
        {
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// ��������� ��������� ��� ������ �������
    /// </summary>
    public void Post()
    {
        if (this.DetailsViewClient.CurrentMode == DetailsViewMode.Insert)
        {
            this.DetailsViewClient.InsertItem(true);
            // ��� ���������� ������� OnInserted � newClientID ������� ��� ��������            
            this.AddonInfo1.Post(this.newClientID);
            this.newClientID = 0;

            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
            if (this.HideClientDetails != null) this.HideClientDetails(this, new EventArgs());
        }
        else if (this.DetailsViewClient.CurrentMode == DetailsViewMode.Edit)
        {
            this.DetailsViewClient.UpdateItem(true);
            this.AddonInfo1.Post(0);
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// �������, ����� ��� ��� ������� ����� �������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceClientDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //������� ���� �������� � ����������� � ����� ��������� ���
        DateTime new_rec = DateTime.Now;
        e.InputParameters["CreationDateTime"] = new_rec;
        e.InputParameters["CreationAgentID"] = Session["CURRENT_AGENT"];
        e.InputParameters["LastModificationDateTime"] = new_rec;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
    }

    /// <summary>
    /// �������, ����� �������������� ��������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceClientDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //������� ���� ����������� � ������� ����� (��������� ���������)
        e.InputParameters["LastModificationDateTime"] = DateTime.Now;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
    }

    /// <summary>
    /// ������� �� ����������� ������� - ����� ID ������ �������
    /// </summary>
    protected void ObjectDataSourceClientDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.newClientID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
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
