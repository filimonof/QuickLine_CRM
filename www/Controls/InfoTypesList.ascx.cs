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

public partial class Controls_InfoTypesList : System.Web.UI.UserControl
{
    /// <summary>
    /// ��� ��������� � ������ ����� (����)
    /// </summary>
    private int selectedTypeInfoID = 0;

    /// <summary>
    /// ��� ��������� � ������ �����
    /// </summary>
    public int SelectedTypeInfoID
    {
        get { return this.selectedTypeInfoID; }        
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// �������� ������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceInfoType.DataBind();
        this.GridViewInfoTypesList.DataBind();
    }

    /// <summary>
    /// ���������� ������� �������� - ����� ����
    /// </summary>
    public event EventHandler TypeInfoSelected;

    /// <summary>
    /// ��������� �������, � ������ ����� ���������� ������ ���
    /// </summary>
    protected void GridViewInfoTypesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.selectedTypeInfoID = this.GridViewInfoTypesList.SelectedValue != null ? (int)this.GridViewInfoTypesList.SelectedValue : 0;
        // ������������� ������� - ����� ��������
        if (this.TypeInfoSelected != null) this.TypeInfoSelected(sender, e);
    }
}
