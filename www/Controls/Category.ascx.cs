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

public partial class Controls_Category : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// �������� ����� �������� � ��������� ������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAddNewCategory_Click(object sender, EventArgs e)
    {
        this.ObjectDataSourceCategory.Insert();
        this.TextBoxNewCategory.Text = string.Empty;
        this.TextBoxNewNum.Text = "100";
    }
}
