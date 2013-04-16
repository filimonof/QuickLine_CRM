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

public partial class Controls_TasksList : System.Web.UI.UserControl
{

    /// <summary>
    /// какому клиенту принадлежит данный контрол СПИСОК ЗАДАЧ, 
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
    /// задача выбранная в списке задач (поле)
    /// </summary>
    private int selectedTaskID = 0;

    /// <summary>
    /// задача выбранная в списке задач
    /// </summary>
    public int SelectedTaskID
    {
        get { return this.selectedTaskID; }
    }

    /// <summary>
    /// загрузка контрола СПИСОК ЗАДАЧ
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack) this.DataBind();        
    }

    /// <summary>
    /// привязка данных в контроле СПИСКЕ ЗАДАЧ
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceTasks.SelectParameters.Clear();
        this.ObjectDataSourceTasks.SelectParameters.Add("ClientID", TypeCode.Int32, this.BindClientID.ToString());
        this.ObjectDataSourceTasks.DataBind();
        this.GridViewTasks.DataBind();
    }

    /// <summary>
    /// объявление события контрола - ВЫБОР ЗАДАЧИ
    /// </summary>
    public event EventHandler TaskSelected;

    /// <summary>
    /// произошло событие, в списке задач выбрана задача
    /// </summary>
    protected void GridViewTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.selectedTaskID = this.GridViewTasks.SelectedValue != null ? (int)this.GridViewTasks.SelectedValue : 0;
        // инициирование события - ВЫБОР ЗАДАЧИ
        if (this.TaskSelected != null) this.TaskSelected(sender, e);
    }
}
