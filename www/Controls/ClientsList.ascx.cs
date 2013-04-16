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

public partial class Controls_ClientsList : System.Web.UI.UserControl
{

    /// <summary>
    /// ������ ��������� � ������ �������� (����)
    /// </summary>
    private int selectedClientID = 0;

    /// <summary>
    /// ������ ��������� � ������ ��������
    /// </summary>
    public int SelectedClientID
    {
        get { return this.selectedClientID; }
    }

    /// <summary>
    /// �������� �������� ������ ��������
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// �������� ������ � �������� ������ ��������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceClients.DataBind();
        this.GridViewClient.DataBind();
    }

    /// <summary>
    /// ���������� ������� �������� - ����� �������
    /// </summary>
    public event EventHandler ClientSelected;

    /// <summary>
    /// ��������� �������, � ������ �������� ������ ������
    /// </summary>
    protected void GridViewClient_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.selectedClientID = this.GridViewClient.SelectedValue != null ? (int)this.GridViewClient.SelectedValue : 0;
        // ������������� ������� - ����� �������
        if (this.ClientSelected != null) this.ClientSelected(sender, e);
    }

}
