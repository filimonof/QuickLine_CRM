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

public partial class Controls_ActivitiesList : System.Web.UI.UserControl
{
    /// <summary>
    /// какому контакту принадлежит данный контрол СПИСОК ДЕЙСТВИЙ, 
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
    /// какой задаче принадлежит данный контрол СПИСОК ДЕЙСТВИЙ, 
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
    /// какому клиенту принадлежит данный контрол СПИСОК ДЕЙСТВИЙ, 
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
    /// действия выбранное в списке действий (поле)
    /// </summary>
    private int selectedActivitieID = 0;

    /// <summary>
    /// действия выбранное в списке действий 
    /// </summary>
    public int SelectedActivitieID
    {
        get { return this.selectedActivitieID; }
    }

    /// <summary>
    /// загрузка контрола СПИСОК ДЕЙСТВИЙ
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack) this.DataBind();        
    }

    /// <summary>
    /// привязка данных в контроле СПИСКЕ ДЕЙСТВИЙ
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceActivitiesList.SelectParameters.Clear();
        this.ObjectDataSourceActivitiesList.SelectParameters.Add("ContactID", TypeCode.Int32, this.BindContactID.ToString());
        this.ObjectDataSourceActivitiesList.SelectParameters.Add("TaskID", TypeCode.Int32, this.BindTaskID.ToString());
        this.ObjectDataSourceActivitiesList.SelectParameters.Add("ClientID", TypeCode.Int32, this.BindClientID.ToString());
        this.ObjectDataSourceActivitiesList.DataBind();
        this.GridViewActivitiesList.DataBind();
    }

    /// <summary>
    /// объявление события контрола - ВЫБОР ДЕЙСТВИЯ
    /// </summary>
    public event EventHandler ActivitiesSelected;

    /// <summary>
    /// произошло событие, в списке действий выбрано действие
    /// </summary>
    protected void GridViewActivitiesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.selectedActivitieID = this.GridViewActivitiesList.SelectedValue != null ? (int)this.GridViewActivitiesList.SelectedValue : 0;
        // инициирование события - ВЫБОР КОНТАКТА
        if (this.ActivitiesSelected != null) this.ActivitiesSelected(sender, e);
    }

}
