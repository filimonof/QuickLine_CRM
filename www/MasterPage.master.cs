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

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.LabelUserName.Text = string.Format("{0}({1})", 
            GetStatics.GetNameAgent(Session["CURRENT_AGENT"]), GetStatics.GetGroupAgent(Session["CURRENT_AGENT"]));
    }

    protected void ButtonFind_Click(object sender, EventArgs e)
    {                     
        Response.Redirect("~/Find.aspx?find=" + this.TextBoxFind.Text);
    }

    protected void LinkButtonExit1_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }
}
