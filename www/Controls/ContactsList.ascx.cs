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

public partial class WebUserControls_ContactsList : System.Web.UI.UserControl
{
    /// <summary>
    /// ������ ������� ����������� ������ ������� ������ ���������, 
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
    /// ������� ��������� � ������ ��������� (����)
    /// </summary>
    private int selectedContactID = 0;

    /// <summary>
    /// ������� ��������� � ������ ���������
    /// </summary>
    public int SelectedContactID
    {
        get { return this.selectedContactID; }        
    }

    /// <summary>
    /// �������� �������� ������ ���������
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack) this.DataBind();        
    }

    /// <summary>
    /// �������� ������ � �������� ������ ���������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceContacts.SelectParameters.Clear();
        this.ObjectDataSourceContacts.SelectParameters.Add("ClientID", TypeCode.Int32, this.BindClientID.ToString());
        this.ObjectDataSourceContacts.DataBind();
        this.GridViewContacts.DataBind();
    }

    /// <summary>
    /// ���������� ������� �������� - ����� ��������
    /// </summary>
    public event EventHandler ContactSelected;

    /// <summary>
    /// ��������� �������, � ������ ��������� ������ �������
    /// </summary>
    protected void GridViewContacts_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.selectedContactID = this.GridViewContacts.SelectedValue != null ? (int)this.GridViewContacts.SelectedValue : 0;
        // ������������� ������� - ����� ��������
        if (this.ContactSelected != null) this.ContactSelected(sender, e);
    }
}
