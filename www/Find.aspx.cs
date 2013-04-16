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

public partial class Find : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //if (PreviousPage != null)
        //{
        //    TextBox tbFind = (TextBox)PreviousPage.FindControl("TextBoxFind");
        //    if (tbFind != null)
        //    {
        //        Label1.Text = tbFind.ToString();
        //    }

        //object find = ViewState["StringFind"];
        //if (find != null)
        //{
        //    Label1.Text = find.ToString();
        //    ViewState["StringFind"] = null;
        //}

        object find = Request.QueryString["find"];
        if (find != null)
        {
            this.Label1.Text = "Поиск по строке: " + find.ToString();
        }
    }
}
