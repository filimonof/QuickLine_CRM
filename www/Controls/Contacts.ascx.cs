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
    /// какому клиенту принадлежит данный контрол КОНТАКТОВ, 
    /// если 0 выводим все контакты
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
    /// загрузка контрола КОНТАКТОВ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.DataBind();
    }

    /// <summary>
    /// привязка данных в контроле КОНТАКТЫ
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();        
        this.ContactsList1.BindClientID = this.BindClientID;
        this.ContactsList1.DataBind();

        //показать параметры фильтрации в Label        
        string s = string.Empty;
        if (this.BindClientID != 0)
            s += " клиент = \"" + GetStatics.GetNameClient(this.BindClientID) + "\"";
        if (s != string.Empty)
            this.LabelContacts1.Text = "Контакты ( " + s + " ) ";
    }

    /// <summary>
    /// произошло событие, выбор контакта для просмотра в контрле ДЕТАЛИЗАЦИЯ КОНТАКТОВ
    /// </summary>    
    protected void ContactsList_ContactSelected(object sender, EventArgs e)
    {
        this.ContactDetails1.CurrentClientID = this.BindClientID;
        this.ContactDetails1.BindContactID = this.ContactsList1.SelectedContactID;
        this.ContactDetails1.DataBind();

        //выводим действия выбранного контакта
        this.Activities1.BindContactID = this.ContactsList1.SelectedContactID;
        this.Activities1.BindTaskID = 0;
        this.Activities1.BindClientID = 0;
        this.Activities1.DataBind();
        this.Activities1.ShowList();

        this.ShowDetail();
        this.TabsTreeCn1.ShowDetailTab();
    }

    /// <summary>
    /// переключиться на СПИСОК КОНТАКТОВ
    /// </summary>
    public void ShowList()
    {
        this.MultiViewContacts.ActiveViewIndex = 0;
        this.TabsTreeCn1.ShowListingTab();
    }

    /// <summary>
    /// переключиться на ДЕТАЛИЗАЦИЮ КОНТАКТА
    /// </summary>
    public void ShowDetail()
    {
        this.MultiViewContacts.ActiveViewIndex = 1;
    }

    /// <summary>
    /// событие - нажата вкладка ПОКАЗАТЬ СПИСОК КОНТАКТОВ
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
    /// произошло событие - выбор вкладки СОЗДАТЬ КОНТАКТ
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
    /// произошло событие, ДЕТАЛИЗАЦИЯ КОНТАКТОВ изъявило желание переключиться на список контактов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContactDetails1_HideContactDetails(object sender, EventArgs e)
    {
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// произошло событие перед добавлением в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContactDetails1_BeforeInsert(object sender, EventArgs e)
    {
        this.Activities1.Visible = false;
    }

    /// <summary>
    /// произошло событие после добавления в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContactDetails1_AfterInsert(object sender, EventArgs e)
    {
        this.Activities1.Visible = true;
    }

    /// <summary>
    /// произошло событие перед редактированием в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContactDetails1_BeforeUpdate(object sender, EventArgs e)
    {
        this.TabsTreeCn1.UnclickedListingTab(false);
    }

    /// <summary>
    /// произошло событие после редактирования в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ContactDetails1_AfterUpdate(object sender, EventArgs e)
    {
        this.TabsTreeCn1.UnclickedListingTab(true);
    }

}
