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

public partial class Login : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        object login = Request.QueryString["login"];
        object pas = Request.QueryString["pas"];
        if (login != null && pas != null)
        {
            this.Autorize(login.ToString(), pas.ToString());
        }
    }

    /// <summary>
    /// ������� - ������� �� ������ "����"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonLogon_Click(object sender, EventArgs e)
    {
        this.Autorize(this.TextBoxLogin.Text, this.TextBoxPassword.Text);
    }

    /// <summary>
    /// ���������� �����������
    /// </summary>
    /// <param name="login">�����</param>
    /// <param name="pas">������</param>
    public void Autorize(string login, string pas)
    {
        Session["CURRENT_AGENT"] = 0;

        int? result;
        DataSet1TableAdapters.AgentsTableAdapter adapterAgents = new DataSet1TableAdapters.AgentsTableAdapter();
        adapterAgents.Agents_Autorization(login, pas, out result);

        if (result != null)
        {
            switch (result)
            {
                case (-1):
                    this.LabelError.Text = "�������� ��������� ������ � ������";
                    break;
                case (0):
                    this.LabelError.Text = "����� ������������";
                    break;
                default:
                    this.LabelError.Text = string.Empty;
                    Session["CURRENT_AGENT"] = result;
                    FormsAuthentication.RedirectFromLoginPage(login, false);
                    break;
            }
        }
        else
        {
            this.LabelError.Text = "������ ��������� ������, ��������� �������";
        }
        //�������������
        //FormsAuthentication.SignOut();
        //FormsAuthentication.RedirectToLoginPage();
        //���
        //Context.User.Identity.Name;
    }

}
