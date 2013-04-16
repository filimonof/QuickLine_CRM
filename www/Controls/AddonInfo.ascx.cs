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

public partial class Controls_AddonInfo : System.Web.UI.UserControl
{
    /// <summary>
    /// � ������ ������� �������� (client, contact, task, activitie)
    /// </summary>
    public string RelationTable
    {
        get
        {
            object obj = ViewState["RELATION_TABLE" + this.UniqueID];
            return obj != null ? obj.ToString() : string.Empty;
        }
        set
        {
            ViewState["RELATION_TABLE" + this.UniqueID] = value;
        }
    }

    /// <summary>
    /// ID ������ (ClientID, ContactID, TaskID, ActivitieID)
    /// </summary>
    public int X_ID
    {
        get
        {
            object obj = ViewState["X_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["X_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// �������� ��������
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// �������� ������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.AddonInfoView1.RelationTable = this.RelationTable;
        this.AddonInfoView1.X_ID = this.X_ID;
        this.AddonInfoView1.DataBind();

        //������� �������������� ����� � �� �������
        //this.AddonInfoEdit1.RelationTable = this.RelationTable;
        //this.AddonInfoEdit1.X_ID = this.X_ID;
        //this.AddonInfoEdit1.DataBind();
    }

    /// <summary>
    /// ������������� �� �������� �������������� ������
    /// </summary>
    public void ShowView()
    {
        this.MultiViewAddon.ActiveViewIndex = 0;
    }

    /// <summary>
    /// ������������� �� �������������� �������������� ������
    /// </summary>
    public void ShowEdit()
    {
        this.MultiViewAddon.ActiveViewIndex = 1;
    }

    /// <summary>
    /// ����� ������
    /// </summary>
    public void New()
    {
        this.AddonInfoEdit1.RelationTable = this.RelationTable;
        this.AddonInfoEdit1.Clear();
        this.ShowEdit();
    }

    /// <summary>
    /// �������������� ������
    /// </summary>
    public void Edit()
    {
        this.AddonInfoEdit1.RelationTable = this.RelationTable;
        this.AddonInfoEdit1.X_ID = this.X_ID;
        this.AddonInfoEdit1.DataBind();
        this.ShowEdit();
    }

    /// <summary>
    /// �������
    /// </summary>
    public void Del()
    {
        //this.DataBind();
    }

    /// <summary>
    /// ������ ��������
    /// </summary>
    public void Cancel()
    {
        this.ShowView();
    }

    /// <summary>
    /// ��������� ������
    /// </summary>
    /// <param name="newID">0-�������������� ������, �����-����� ������</param>
    public void Post(int newID)
    {
        this.ShowView();        
        this.AddonInfoEdit1.SaveData(newID);
        this.DataBind();
    }
}
