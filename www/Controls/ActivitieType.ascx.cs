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

public partial class Controls_ActivitieTypel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// добавить новые щначени€ и обнулить €чейки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonAddNewType_Click(object sender, EventArgs e)
    {
        this.ObjectDataSourceActivitieType.Insert();
        this.TextBoxNewType.Text = string.Empty;
        this.TextBoxNewNum.Text = "100";
    }

}
