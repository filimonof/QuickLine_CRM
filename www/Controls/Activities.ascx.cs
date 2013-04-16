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

public partial class Controls_Activities : System.Web.UI.UserControl
{
    /// <summary>
    /// какому контакту принадлежит данный контрол ДЕЙСТВИЕ, 
    /// если 0 выводим все действия
    /// </summary>    
    public int BindContactID
    {
        get
        {
            object obj = ViewState["CONTACT_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["CONTACT_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// какой задаче принадлежит данный контрол ДЕЙСТВИЕ, 
    /// если 0 выводим все действия
    /// </summary>    
    public int BindTaskID
    {
        get
        {
            object obj = ViewState["TASK_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["TASK_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// какому клиенту принадлежит данный контрол ДЕЙСТВИЕ, 
    /// если 0 выводим все действия
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
    /// загрузка контрола ДЕЙСТВИЯ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack) this.DataBind();
    }

    /// <summary>
    /// привязка данных в контроле ДЕЙСТВИЕ
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();        
        this.ActivitiesList1.BindContactID = this.BindContactID;
        this.ActivitiesList1.BindTaskID = this.BindTaskID;
        this.ActivitiesList1.BindClientID = this.BindClientID;
        this.ActivitiesList1.DataBind();

        //показать параметры фильтрации в Label        
        string s = string.Empty;
        if (this.BindClientID != 0)
            s += " клиент = \"" + GetStatics.GetNameClient(this.BindClientID) + "\"";
        if (this.BindTaskID != 0)
            s += (s == string.Empty ? string.Empty : ",") + " задача = \"" + GetStatics.GetNameTask(this.BindTaskID) + "\"";
        if (this.BindContactID != 0)
            s += (s == string.Empty ? string.Empty : ",") + " контакт = \"" + GetStatics.GetNameContact(this.BindContactID) + "\"";
        if (s != string.Empty)
            this.LabelActivities1.Text = "Действия ( " + s + " ) ";
    }

    /// <summary>
    /// произошло событие, выбор действия для просмотра в контрле ДЕТАЛИЗАЦИЯ ДЕЙСТВИЯ
    /// </summary>    
    protected void ActivitiesList1_ActivitiesSelected(object sender, EventArgs e)
    {
        this.ActivitieDetails1.CurrentContactID = this.BindContactID;
        this.ActivitieDetails1.CurrentTaskID = this.BindTaskID;
        this.ActivitieDetails1.CurrentClientID = this.BindClientID;
        this.ActivitieDetails1.BindActivitieID = this.ActivitiesList1.SelectedActivitieID;
        this.ActivitieDetails1.DataBind();
        this.ShowDetail();
        this.TabsTreeA1.ShowDetailTab();
    }

    /// <summary>
    /// переключиться на СПИСОК ДЕЙСТВИЙ
    /// </summary>
    public void ShowList()
    {
        this.MultiViewActivities.ActiveViewIndex = 0;
        this.TabsTreeA1.ShowListingTab();
    }

    /// <summary>
    /// переключиться на ДЕТАЛИЗАЦИЮ ДЕЙСТВИЯ
    /// </summary>
    public void ShowDetail()
    {
        this.MultiViewActivities.ActiveViewIndex = 1;
    }

    /// <summary>
    /// событие - нажата вкладка ПОКАЗАТЬ СПИСОК ДЕЙСТВИЙ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeA1_ClickListing(object sender, EventArgs e)
    {
        this.ActivitieDetails1.Cancel();
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// произошло событие - выбор вкладки СОЗДАТЬ ДЕЙСТВИЕ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeA1_ClickNew(object sender, EventArgs e)
    {
        this.ActivitieDetails1.CurrentContactID = this.BindContactID;
        this.ActivitieDetails1.CurrentTaskID = this.BindTaskID;
        this.ActivitieDetails1.CurrentClientID = this.BindClientID;
        this.ActivitieDetails1.New();
        this.ShowDetail();
        this.TabsTreeA1.ShowNewTab();
    }

    /// <summary>
    /// произошло событие, ДЕТАЛИЗАЦИЯ ДЕЙСТВИЯ изъявило желание переключиться на список действий
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ActivitieDetails1_HideActivitieDetails(object sender, EventArgs e)
    {
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// произошло событие перед редактированием в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ActivitieDetails1_BeforeUpdate(object sender, EventArgs e)
    {
        this.TabsTreeA1.UnclickedListingTab(false);
    }

    /// <summary>
    /// произошло событие после редактирования в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ActivitieDetails1_AfterUpdate(object sender, EventArgs e)
    {
        this.TabsTreeA1.UnclickedListingTab(true);
    }

}
