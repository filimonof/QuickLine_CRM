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

public partial class Controls_TasksList : System.Web.UI.UserControl
{

    /// <summary>
    /// ������ ������� ����������� ������ ������� ������ �����, 
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
    /// ������ ��������� � ������ ����� (����)
    /// </summary>
    private int selectedTaskID = 0;

    /// <summary>
    /// ������ ��������� � ������ �����
    /// </summary>
    public int SelectedTaskID
    {
        get { return this.selectedTaskID; }
    }

    /// <summary>
    /// �������� �������� ������ �����
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack) this.DataBind();        
    }

    /// <summary>
    /// �������� ������ � �������� ������ �����
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceTasks.SelectParameters.Clear();
        this.ObjectDataSourceTasks.SelectParameters.Add("ClientID", TypeCode.Int32, this.BindClientID.ToString());
        this.ObjectDataSourceTasks.DataBind();
        this.GridViewTasks.DataBind();
    }

    /// <summary>
    /// ���������� ������� �������� - ����� ������
    /// </summary>
    public event EventHandler TaskSelected;

    /// <summary>
    /// ��������� �������, � ������ ����� ������� ������
    /// </summary>
    protected void GridViewTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.selectedTaskID = this.GridViewTasks.SelectedValue != null ? (int)this.GridViewTasks.SelectedValue : 0;
        // ������������� ������� - ����� ������
        if (this.TaskSelected != null) this.TaskSelected(sender, e);
    }
}
