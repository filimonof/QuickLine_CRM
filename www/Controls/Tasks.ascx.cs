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

public partial class Controls_Tasks : System.Web.UI.UserControl
{

    /// <summary>
    /// какому клиенту принадлежит данный контрол «јƒј„», 
    /// если 0 выводим все задачи
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
    /// загрузка контрола «јƒј„»
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.DataBind();
    }

    /// <summary>
    /// прив€зка данных в контроле «јƒј„»
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();       
        this.TasksList1.BindClientID = this.BindClientID;
        this.TasksList1.DataBind();

        //показать параметры фильтрации в Label        
        string s = string.Empty;
        if (this.BindClientID != 0)
            s += " клиент = \"" + GetStatics.GetNameClient(this.BindClientID) + "\"";
        if (s != string.Empty)
            this.LabelTasks1.Text = "«адачи ( " + s + " ) ";
    }

    /// <summary>
    /// произошло событие, выбор контакта дл€ просмотра в контрле ƒ≈“јЋ»«ј÷»я «јƒј„»
    /// </summary>    
    protected void TasksList1_TaskSelected(object sender, EventArgs e)
    {
        this.TaskDetails1.CurrentClientID = this.BindClientID;
        this.TaskDetails1.BindTaskID = this.TasksList1.SelectedTaskID;
        this.TaskDetails1.DataBind();

        //выводим действи€ выбранной задачи
        this.Activities2.BindContactID = 0;
        this.Activities2.BindTaskID = this.TasksList1.SelectedTaskID;
        this.Activities2.BindClientID = 0;
        this.Activities2.DataBind();
        this.Activities2.ShowList();

        this.ShowDetail();
        this.TabsTreeT1.ShowDetailTab();
    }

    /// <summary>
    /// переключитьс€ на —ѕ»—ќ  «јƒј„
    /// </summary>
    public void ShowList()
    {
        this.MultiViewTasks.ActiveViewIndex = 0;
        this.TabsTreeT1.ShowListingTab();
    }

    /// <summary>
    /// переключитьс€ на ƒ≈“јЋ»«ј÷»ё «јƒј„»
    /// </summary>
    public void ShowDetail()
    {
        this.MultiViewTasks.ActiveViewIndex = 1;
    }

    /// <summary>
    /// событие - нажата вкладка ѕќ ј«ј“№ —ѕ»—ќ  «јƒј„
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeT1_ClickListing(object sender, EventArgs e)
    {
        this.TaskDetails1.Cancel();
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// произошло событие - выбор вкладки —ќ«ƒј“№ «јƒј„”
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeT1_ClickNew(object sender, EventArgs e)
    {
        this.TaskDetails1.CurrentClientID = this.BindClientID;
        this.TaskDetails1.New();
        this.ShowDetail();
        this.TabsTreeT1.ShowNewTab();
    }

    /// <summary>
    /// произошло событие, ƒ≈“јЋ»«ј÷»я «јƒј„ћ изъ€вило желание переключитьс€ на список задач
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskDetails1_HideTaskDetails(object sender, EventArgs e)
    {
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// произошло событие перед добавлением в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskDetails1_BeforeInsert(object sender, EventArgs e)
    {
        this.Activities2.Visible = false;
    }

    /// <summary>
    /// произошло событие после добавлени€ в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskDetails1_AfterInsert(object sender, EventArgs e)
    {
        this.Activities2.Visible = true;
    }

    /// <summary>
    /// произошло событие перед редактированием в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskDetails1_BeforeUpdate(object sender, EventArgs e)
    {
        this.TabsTreeT1.UnclickedListingTab(false);
    }

    /// <summary>
    /// произошло событие после редактировани€ в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskDetails1_AfterUpdate(object sender, EventArgs e)
    {
        this.TabsTreeT1.UnclickedListingTab(true);
    }

}
