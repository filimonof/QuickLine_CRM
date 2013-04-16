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

public partial class Controls_TabsTwo : System.Web.UI.UserControl
{
    private int activeTab = 0;
    // ����� ������� �������
    public int ActiveTab
    {
        get { return this.activeTab; }
        set
        {
            switch (this.activeTab)
            {
                case (0):
                default:
                    this.ShowListingTab();
                    break;
                case (1):
                    this.ShowNewTab();
                    break;
                case (2):
                    this.ShowDetailTab();
                    break;
            }
        }
    }

    //����� �������
    private int widthTabListing = 100;
    private int widthTabNew = 100;
    private int widthTabDetail = 100;
    public int WidthTabListing
    {
        get { return this.widthTabListing; }
        set { this.widthTabListing = value; }
    }
    public int WidthTabNew
    {
        get { return this.widthTabNew; }
        set { this.widthTabNew = value; }
    }
    public int WidthTabDetail
    {
        get { return this.widthTabDetail; }
        set { this.widthTabDetail = value; }
    }

    //�������� �������
    private string nameTabListing = "������";
    private string nameTabNew = "�������";
    private string nameTabDetail = "�����������";
    public string NameTabListing
    {
        get { return this.nameTabListing; }
        set { this.nameTabListing = value; }
    }
    public string NameTabNew
    {
        get { return this.nameTabNew; }
        set { this.nameTabNew = value; }
    }
    public string NameTabDetail
    {
        get { return this.nameTabDetail; }
        set { this.nameTabDetail = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LinkTab1.Text = this.NameTabListing;
            this.LinkTab2.Text = this.NameTabNew;
            this.LinkTab3.Text = this.NameTabDetail;
            this.TabFon1.Width = this.WidthTabListing;
            this.TabFon2.Width = this.WidthTabNew;
            this.TabFon3.Width = this.WidthTabDetail;
            this.ShowListingTab();
        }
    }

    /// <summary>
    /// ������������� �� ������� �� �������
    /// </summary>
    public void ShowListingTab()
    {
        this.activeTab = 0;

        this.LinkTab1.Enabled = false;
        this.LinkTab1.Font.Bold = true;
        this.ImageTabLeft1.ImageUrl = "~/Images/Tabs/Tabs_Active_Left.gif";
        this.ImageTabRight1.ImageUrl = "~/Images/Tabs/Tabs_Active_Right.gif";
        this.TabFon1.Style.Clear();
        this.TabFon1.Style.Add("background-image", "url(Images/Tabs/Tabs_Active_Fon.gif)");

        this.LinkTab2.Enabled = true;
        this.LinkTab2.Font.Bold = false;
        this.ImageTabLeft2.ImageUrl = "~/Images/Tabs/Tabs_Deactive_Left.gif";
        this.ImageTabRight2.ImageUrl = "~/Images/Tabs/Tabs_Deactive_Right.gif";
        this.TabFon2.Style.Clear();
        this.TabFon2.Style.Add("background-image", "url(Images/Tabs/Tabs_Deactive_Fon.gif)");

        this.LinkTab3.Visible = false;
        this.ImageTabLeft3.Visible = false;
        this.ImageTabRight3.Visible = false;
        this.TabLeft3.Style.Clear();
        this.TabLeft3.Style.Add("background-image", "url(Images/Tabs/Tabs_Up.gif)");
        this.TabRight3.Style.Clear();
        this.TabRight3.Style.Add("background-image", "url(Images/Tabs/Tabs_Up.gif)");
        this.TabFon3.Style.Clear();
        this.TabFon3.Style.Add("background-image", "url(Images/Tabs/Tabs_Up.gif)");

    }

    /// <summary>
    /// ������������� �� ������� � ������ �������
    /// </summary>
    public void ShowNewTab()
    {
        this.activeTab = 1;

        this.LinkTab1.Enabled = false;
        this.LinkTab1.Font.Bold = false;
        this.ImageTabLeft1.ImageUrl = "~/Images/Tabs/Tabs_Deactive_Left.gif";
        this.ImageTabRight1.ImageUrl = "~/Images/Tabs/Tabs_Deactive_Right.gif";
        this.TabFon1.Style.Clear();
        this.TabFon1.Style.Add("background-image", "url(Images/Tabs/Tabs_Deactive_Fon.gif)");

        this.LinkTab2.Enabled = false;
        this.LinkTab2.Font.Bold = true;
        this.ImageTabLeft2.ImageUrl = "~/Images/Tabs/Tabs_Active_Left.gif";
        this.ImageTabRight2.ImageUrl = "~/Images/Tabs/Tabs_Active_Right.gif";
        this.TabFon2.Style.Clear();
        this.TabFon2.Style.Add("background-image", "url(Images/Tabs/Tabs_Active_Fon.gif)");

        this.LinkTab3.Visible = false;
        this.ImageTabLeft3.Visible = false;
        this.TabLeft3.Style.Clear();
        this.TabLeft3.Style.Add("background-image", "url(Images/Tabs/Tabs_Up.gif)");
        this.ImageTabRight3.Visible = false;
        this.TabRight3.Style.Clear();
        this.TabRight3.Style.Add("background-image", "url(Images/Tabs/Tabs_Up.gif)");
        this.TabFon3.Style.Clear();
        this.TabFon3.Style.Add("background-image", "url(Images/Tabs/Tabs_Up.gif)");
    }

    /// <summary>
    /// ������������� �� ������� � ������������
    /// </summary>
    public void ShowDetailTab()
    {
        this.activeTab = 2;

        this.LinkTab1.Enabled = true;
        this.LinkTab1.Font.Bold = false;
        this.ImageTabLeft1.ImageUrl = "~/Images/Tabs/Tabs_Deactive_Left.gif";
        this.ImageTabRight1.ImageUrl = "~/Images/Tabs/Tabs_Deactive_Right.gif";
        this.TabFon1.Style.Clear();
        this.TabFon1.Style.Add("background-image", "url(Images/Tabs/Tabs_Deactive_Fon.gif)");

        this.LinkTab2.Enabled = true;
        this.LinkTab2.Font.Bold = false;
        this.ImageTabLeft2.ImageUrl = "~/Images/Tabs/Tabs_Deactive_Left.gif";
        this.ImageTabRight2.ImageUrl = "~/Images/Tabs/Tabs_Deactive_Right.gif";
        this.TabFon2.Style.Clear();
        this.TabFon2.Style.Add("background-image", "url(Images/Tabs/Tabs_Deactive_Fon.gif)");

        this.LinkTab3.Visible = true;
        this.LinkTab3.Enabled = false;
        this.LinkTab3.Font.Bold = true;
        this.ImageTabLeft3.Visible = true;
        this.ImageTabLeft3.ImageUrl = "~/Images/Tabs/Tabs_Active_Left.gif";
        this.TabLeft3.Style.Clear();
        this.ImageTabRight3.Visible = true;
        this.ImageTabRight3.ImageUrl = "~/Images/Tabs/Tabs_Active_Right.gif";
        this.TabRight3.Style.Clear();
        this.TabFon3.Style.Clear();
        this.TabFon3.Style.Add("background-image", "url(Images/Tabs/Tabs_Active_Fon.gif)");
    }

    /// <summary>
    /// ��������� �������� ����������
    /// </summary>
    /// <param name="isClicked">false - ��������� �������, true - ����������</param>
    public void UnclickedListingTab(bool isClicked)
    {
        this.LinkTab1.Enabled = isClicked;
        this.LinkTab2.Enabled = isClicked;
    }

    #region ������������ ��������� �������
    ///// <summary>
    ///// ������������� �� ������� numTab
    ///// </summary>
    ///// <param name="numTab">����� �������</param>
    //public void SetActiveTab(int numTab)
    //{
    //}

    ///// <summary>
    ///// ������� ������� �� �������
    ///// </summary>
    ///// <param name="numTab">����� �������</param>
    //public void HideTab(int numTab)
    //{
    //}

    ///// <summary>
    ///// �������� ����������� ������� �� ������ numTab 
    ///// </summary>
    ///// <param name="numTab">����� �������</param>
    //public void UnclickedTab(int numTab)
    //{
    //}
    #endregion

    /// <summary>
    /// ������� - ������� ������� 1 - ������
    /// </summary>
    public event EventHandler ClickListing;

    /// <summary>
    /// ������� - ������� ������� 2 - ������� �����
    /// </summary>
    public event EventHandler ClickNew;

    protected void LinkTab1_Click(object sender, EventArgs e)
    {
        if (this.ClickListing != null) this.ClickListing(this, new EventArgs());
    }

    protected void LinkTab2_Click(object sender, EventArgs e)
    {
        if (this.ClickNew != null) this.ClickNew(this, new EventArgs());
    }
}
