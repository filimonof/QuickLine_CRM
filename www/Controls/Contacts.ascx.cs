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

public partial class WebUserControls_Contacts : System.Web.UI.UserControl
{

    /// <summary>
    /// ������ ������� ����������� ������ ������� ���������, 
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
    /// �������� �������� ���������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.DataBind();
    }

    /// <summary>
    /// �������� ������ � �������� ��������
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();        
        this.ContactsList1.BindClientID = this.BindClientID;
        this.ContactsList1.DataBind();

        //�������� ��������� ���������� � Label        
        string s = string.Empty;
        if (this.BindClientID != 0)
            s += " ������ = \"" + GetStatics.GetNameClient(this.BindClientID) + "\"";
        if (s != string.Empty)
            this.LabelContacts1.Text = "�������� ( " + s + " ) ";
    }

    /// <summary>
    /// ��������� �������, ����� �������� ��� ��������� � ������� ����������� ���������
    /// </summary>    
    protected void ContactsList_ContactSelected(object sender, EventArgs e)
    {
        this.ContactDetails1.CurrentClientID = this.BindClientID;
        this.ContactDetails1.BindContactID = this.ContactsList1.SelectedContactID;
        this.ContactDetails1.DataBind();

        //������� �������� ���������� ��������
        this.Activities1.BindContactID = this.ContactsList1.SelectedContactID;
        this.Activities1.BindTaskID = 0;
        this.Activities1.BindClientID = 0;
        this.Activities1.DataBind();
        this.Activities1.ShowList();

        this.ShowDetail();
        this.TabsTreeCn1.ShowDetailTab();
    }

    /// <summary>
    /// ������������� �� ������ ���������
    /// </summary>
    public void ShowList()
    {
        this.MultiViewContacts.ActiveViewIndex = 0;
        this.TabsTreeCn1.ShowListingTab();
    }

    /// <summary>
    /// ������������� �� ����������� ��������
    /// </summary>
    public void ShowDetail()
    {
        this.MultiViewContacts.ActiveViewIndex = 1;
    }

    /// <summary>
    /// ������� - ������ ������� �������� ������ ���������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeCn1_ClickListing(object sender, EventArgs e)
    {
        this.ContactDetails1.Cancel();
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// ��������� ������� - ����� ������� ������� �������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeCn1_ClickNew(object sender, EventArgs e)
    {
        this.ContactDetails1.CurrentClientID = this.BindClientID;
        this.ContactDetails1.New();
        this.ShowDetail();
        this.TabsTreeCn1.ShowNewTab();
    }

    /// <summary>
    /// ��������� �������, ����������� ��������� �������� ������� ������������� �� ������ ���������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContactDetails1_HideContactDetails(object sender, EventArgs e)
    {
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// ��������� ������� ����� ����������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContactDetails1_BeforeInsert(object sender, EventArgs e)
    {
        this.Activities1.Visible = false;
    }

    /// <summary>
    /// ��������� ������� ����� ���������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContactDetails1_AfterInsert(object sender, EventArgs e)
    {
        this.Activities1.Visible = true;
    }

    /// <summary>
    /// ��������� ������� ����� ��������������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContactDetails1_BeforeUpdate(object sender, EventArgs e)
    {
        this.TabsTreeCn1.UnclickedListingTab(false);
    }

    /// <summary>
    /// ��������� ������� ����� �������������� � �����������
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContactDetails1_AfterUpdate(object sender, EventArgs e)
    {
        this.TabsTreeCn1.UnclickedListingTab(true);
    }

}
