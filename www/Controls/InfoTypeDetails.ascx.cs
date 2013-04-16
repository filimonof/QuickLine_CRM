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

public partial class Controls_InfoTypeDetails : System.Web.UI.UserControl
{
    /// <summary>
    /// � ����� ���� ���������� ������� ��������� ����������
    /// </summary>   
    public int BindTypeInfoID
    {
        get
        {
            object obj = ViewState["TYPEINFO_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["TYPEINFO_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// ������ ��� ��������� ID ���� ��� ���������� ������
    /// </summary>
    private int newInfoTypeID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// �������� ������ � �������� ����������� ����� ����������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceInfoTypeToID.SelectParameters.Clear();
        this.ObjectDataSourceInfoTypeToID.SelectParameters.Add("ID", TypeCode.Int32, this.BindTypeInfoID.ToString());
        this.ObjectDataSourceInfoTypeToID.DataBind();
        this.DetailsViewTypeInfo.DataBind();

        this.InfoTypeCombo1.BindTypeInfoID = this.BindTypeInfoID;
        this.InfoTypeCombo1.DataBind();
    }

    /// <summary>
    /// ����� ��� ����������
    /// </sumary>
    public void New()
    {
        if (this.BeforeInsert != null) this.BeforeInsert(this, new EventArgs());
        this.DetailsViewTypeInfo.ChangeMode(DetailsViewMode.Insert);
    }

    /// <summary>
    /// ������������� ������� ������
    /// </summary>
    public void Edit()
    {
        if (this.BeforeUpdate != null) this.BeforeUpdate(this, new EventArgs());
        this.DetailsViewTypeInfo.ChangeMode(DetailsViewMode.Edit);
    }

    #region ���������� �������

    /// <summary>
    /// ������� - ����� �� �����������
    /// </summary>
    public event EventHandler HideInfoTypeDetails;

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
        this.DetailsViewTypeInfo.ChangeMode(DetailsViewMode.ReadOnly);
        this.DetailsViewTypeInfo.DeleteItem();
        this.InfoTypeCombo1.Del();
        //����� ��� ����� �������� �������� ������ �������� ���������
        if (this.HideInfoTypeDetails != null) this.HideInfoTypeDetails(this, new EventArgs());
    }

    /// <summary>
    /// �������� �������������� ��� �������� ������
    /// </summary>
    public void Cancel()
    {
        bool inserted = this.DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Insert;
        this.DetailsViewTypeInfo.ChangeMode(DetailsViewMode.ReadOnly);
        this.InfoTypeCombo1.Del();
        if (inserted)
        {
            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
        }
        else
        {
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }

        //����� ��� ����� �������� �������� ������ �������� ���������
        if (this.HideInfoTypeDetails != null) this.HideInfoTypeDetails(this, new EventArgs());        
    }

    /// <summary>
    /// ��������� ��������� ��� ����� ������
    /// </summary>
    public void Post()
    {
        if (this.DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Insert)
        {
            this.DetailsViewTypeInfo.InsertItem(true);
            // ��� ���������� ������� OnInserted � newTaskID ������� ��� ��������     
            this.InfoTypeCombo1.SaveNew(this.newInfoTypeID);
            this.newInfoTypeID = 0;
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
        else if (this.DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Edit)
        {
            this.DetailsViewTypeInfo.UpdateItem(true);
            this.InfoTypeCombo1.SaveEdit();
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }

        //this.InfoTypeCombo1.DataBind();

        //����� ��� ����� �������� �������� ������ �������� ���������
        if (this.HideInfoTypeDetails != null) this.HideInfoTypeDetails(this, new EventArgs());
    }

    #region ������� �� ������ �������������� ������

    protected void ButtonPost_Click(object sender, EventArgs e)
    {
        this.Post();
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        this.Cancel();
    }
    protected void ButtonDel_Click(object sender, EventArgs e)
    {
        this.Del();
    }
    protected void ButtonDel_PreRender(object sender, EventArgs e)
    {
        this.ButtonDel.Visible = this.DetailsViewTypeInfo.CurrentMode != DetailsViewMode.Insert;
    }

    #endregion

    /// <summary>
    /// ������� �� ����������� ���� - ����� ID ������ ����
    /// </summary>
    protected void ObjectDataSourceInfoTypeToID_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.newInfoTypeID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
    }

    /// <summary>
    /// ������� �� ��������� ���� ������ ��� ��������������
    /// </summary>
    protected void DropDownListTypeInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //todo: ����� ������� ����� ��� ��������� �������� �������� ������ ���� �����������, ������ ��?
        if (((DropDownList)sender).SelectedValue == TypeInfo.TI_COMBO)
            this.InfoTypeCombo1.Show(this.DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Insert);
        else
            this.InfoTypeCombo1.Hide();
    }

    /// <summary>
    /// ������� �� �������� ������ � ���������� � ����� ������
    /// </summary>
    protected void DropDownListTypeInfo_DataBound(object sender, EventArgs e)
    {
        this.DropDownListTypeInfo_SelectedIndexChanged(sender, e);
    }

}
