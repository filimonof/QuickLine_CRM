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

public partial class Controls_AddonInfo : System.Web.UI.UserControl
{
    /// <summary>
    /// с какими данными работаем (client, contact, task, activitie)
    /// </summary>
    public string RelationTable
    {
        get
        {
            object obj = ViewState["RELATION_TABLE" + this.UniqueID];
            return obj != null ? obj.ToString() : string.Empty;
        }
        set
        {
            ViewState["RELATION_TABLE" + this.UniqueID] = value;
        }
    }

    /// <summary>
    /// ID данных (ClientID, ContactID, TaskID, ActivitieID)
    /// </summary>
    public int X_ID
    {
        get
        {
            object obj = ViewState["X_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["X_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// загрузка страницы
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// привязка данных
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.AddonInfoView1.RelationTable = this.RelationTable;
        this.AddonInfoView1.X_ID = this.X_ID;
        this.AddonInfoView1.DataBind();

        //таблицу редактирования можно и не биндить
        //this.AddonInfoEdit1.RelationTable = this.RelationTable;
        //this.AddonInfoEdit1.X_ID = this.X_ID;
        //this.AddonInfoEdit1.DataBind();
    }

    /// <summary>
    /// переключиться на ПРОСМОТР дополнительных данных
    /// </summary>
    public void ShowView()
    {
        this.MultiViewAddon.ActiveViewIndex = 0;
    }

    /// <summary>
    /// переключиться на РЕДАКТИРОВАНИЕ дополнительных данных
    /// </summary>
    public void ShowEdit()
    {
        this.MultiViewAddon.ActiveViewIndex = 1;
    }

    /// <summary>
    /// новые данные
    /// </summary>
    public void New()
    {
        this.AddonInfoEdit1.RelationTable = this.RelationTable;
        this.AddonInfoEdit1.Clear();
        this.ShowEdit();
    }

    /// <summary>
    /// редактирование данных
    /// </summary>
    public void Edit()
    {
        this.AddonInfoEdit1.RelationTable = this.RelationTable;
        this.AddonInfoEdit1.X_ID = this.X_ID;
        this.AddonInfoEdit1.DataBind();
        this.ShowEdit();
    }

    /// <summary>
    /// удалить
    /// </summary>
    public void Del()
    {
        //this.DataBind();
    }

    /// <summary>
    /// отмена действия
    /// </summary>
    public void Cancel()
    {
        this.ShowView();
    }

    /// <summary>
    /// сохранить данные
    /// </summary>
    /// <param name="newID">0-редактирование данных, иначе-новые данные</param>
    public void Post(int newID)
    {
        this.ShowView();        
        this.AddonInfoEdit1.SaveData(newID);
        this.DataBind();
    }
}
