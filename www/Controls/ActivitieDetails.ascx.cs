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

public partial class Controls_ActivitieDetails : System.Web.UI.UserControl
{

    /// <summary>
    /// о каком действии выводим детальную информацию
    /// </summary>   
    public int BindActivitieID
    {
        get
        {
            object obj = ViewState["ACTIVITIE_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["ACTIVITIE_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// текущий контакт, тот которому принадлежит данное действие
    /// </summary>
    public int CurrentContactID
    {
        get
        {
            object obj = ViewState["CURRENT_CONTACT_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["CURRENT_CONTACT_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// текуща€ задача, та которой принадлежит данное действие
    /// </summary>
    public int CurrentTaskID
    {
        get
        {
            object obj = ViewState["CURRENT_TASK_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["CURRENT_TASK_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// текущий клиент, тот которому принадлежит данное действие
    /// </summary>
    public int CurrentClientID
    {
        get
        {
            object obj = ViewState["CURRENT_CLIENT_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["CURRENT_CLIENT_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// true - если все три Current..ID равны 0
    /// значит контрл действи€ открыт напр€мую без зависимостей
    /// текущего клиента придетс€ узнавать через текущие значени€ полей TaskID ContactID 
    /// </summary>
    protected bool IsAllCurrentZero
    {
        get { return this.CurrentClientID == 0 && this.CurrentTaskID == 0 && this.CurrentContactID == 0; }
    }

    /// <summary>
    /// к какому клиенту относитс€ данный контрол ƒействий
    /// данное свойство имеет смысл, тлько если хот€бы один из current-ов неравен нулю
    /// если LinkedClient=0 то узнать текущего клиента можно только то текущим значени€м полей TaskID ContactID
    /// </summary>
    protected int LinkedClient
    {
        get { return GetStatics.GetClientID(CurrentClientID, CurrentTaskID, CurrentContactID); }
    }

    /// <summary>
    /// служет дл€ узнавани€ ID действи€ при добавлении нового
    /// </summary> 
    private int newActivitieID = 0;

    /// <summary>
    /// загрузка контрола ƒ≈“јЋ»«ј÷»я ƒ≈…—“¬»я
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// прив€зка данных в контроле ƒ≈“јЋ»«ј÷»я ƒ≈…—“¬»я
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceActivitieDetails.SelectParameters.Clear();
        this.ObjectDataSourceActivitieDetails.SelectParameters.Add("ID", TypeCode.Int32, this.BindActivitieID.ToString());
        this.ObjectDataSourceActivitieDetails.DataBind();
        this.DetailsViewActivitieDetails.DataBind();

        this.AddonInfo3.RelationTable = TypeRelation.TR_ACTIVITIE;
        this.AddonInfo3.X_ID = this.BindActivitieID;
        this.AddonInfo3.DataBind();
        this.AddonInfo3.ShowView();

        this.FilteredTaskAndContact(this.LinkedClient);
    }

    /// <summary>
    /// обновление видимости кнопок редактировани€
    /// </summary>
    private void UpdateButton()
    {
        this.ButtonEdit.Visible = this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.ReadOnly;
        this.ButtonDel.Visible = this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.ReadOnly;

        this.ButtonPost.Visible =
            (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Edit);
        this.ButtonCancel.Visible =
            (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Edit);
    }

    #region Ќажати€ кнопок

    /// <summary>
    /// нажата кнопка –≈ƒј “»–ќ¬јЌ» 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        this.Edit();
    }

    /// <summary>
    /// нажата кнопка ќ“ћ≈Ќј 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        this.Cancel();
    }

    /// <summary>
    /// нажата кнопка ”ƒјЋ»“№ 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonDel_Click(object sender, EventArgs e)
    {
        this.Del();
    }

    /// <summary>
    /// нажата кнопка —ќ’–јЌ»“№ 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonPost_Click(object sender, EventArgs e)
    {
        this.Post();
    }

    #endregion

    /// <summary>
    /// событие перед прорисовкой кнопок
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_PreRender(object sender, EventArgs e)
    {
        this.UpdateButton();
    }

    /// <summary>
    /// событие, ѕ≈–≈ƒ –≈ƒј “»–ќ¬јЌ»≈ћ действи€
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceActivitieDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //текуща€ дата модификации и текущий агент (сделавший изменени€)
        e.InputParameters["LastModificationDateTime"] = DateTime.Now;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
    }

    /// <summary>
    /// событие, ѕ≈–≈ƒ тем как ƒќЅј¬»“№ новое действие
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param> 
    protected void ObjectDataSourceActivitieDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //текуща€ дата создани€ и модификации и агент сделавший это
        DateTime new_rec = DateTime.Now;
        e.InputParameters["CreationDateTime"] = new_rec;
        e.InputParameters["CreationAgentID"] = Session["CURRENT_AGENT"];
        e.InputParameters["LastModificationDateTime"] = new_rec;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
        if (this.CurrentContactID != 0)
            e.InputParameters["ContactID"] = this.CurrentContactID;
        if (this.CurrentTaskID != 0)
            e.InputParameters["TaskID"] = this.CurrentTaskID;
    }

    /// <summary>
    /// событие на добапвление действи€ - узнаЄм ID нового действи€
    /// </summary>
    protected void ObjectDataSourceActivitieDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.newActivitieID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
    }

    /// <summary>
    /// Ќќ¬џќ≈ действие
    /// </sumary>
    public void New()
    {
        if (this.BeforeInsert != null) this.BeforeInsert(this, new EventArgs());
        this.DetailsViewActivitieDetails.ChangeMode(DetailsViewMode.Insert);

        this.AddonInfo3.RelationTable = TypeRelation.TR_ACTIVITIE;
        this.AddonInfo3.New();

        if (!this.IsAllCurrentZero)
            this.FilteredTaskAndContact(this.LinkedClient);
    }

    /// <summary>
    /// –≈ƒј “»–ќ¬ј“№ текущее действие
    /// </summary>
    public void Edit()
    {
        if (this.BeforeUpdate != null) this.BeforeUpdate(this, new EventArgs());
        this.DetailsViewActivitieDetails.ChangeMode(DetailsViewMode.Edit);
        this.AddonInfo3.Edit();

        if (!this.IsAllCurrentZero)
            this.FilteredTaskAndContact(this.LinkedClient);
    }

    #region ќбъ€вление событий

    /// <summary>
    /// событие - ¬џ…“» из детализации
    /// </summary>
    public event EventHandler HideActivitieDetails;

    /// <summary>
    /// событие - ѕ≈–≈ƒ добавлением
    /// </summary>
    public event EventHandler BeforeInsert;
    /// <summary>
    /// событие - ѕќ—Ћ≈ добавлени€
    /// </summary>
    public event EventHandler AfterInsert;

    /// <summary>
    /// событие - ѕ≈–≈ƒ редактированием
    /// </summary>
    public event EventHandler BeforeUpdate;
    /// <summary>
    /// событие - ѕќ—Ћ≈ редактированием
    /// </summary>
    public event EventHandler AfterUpdate;

    #endregion

    /// <summary>
    /// ”ƒјЋ»“№ текущее действие
    /// </summary>
    public void Del()
    {
        this.DetailsViewActivitieDetails.DeleteItem();
        this.AddonInfo3.Del();
        if (this.HideActivitieDetails != null) this.HideActivitieDetails(this, new EventArgs());
    }

    /// <summary>
    /// ќ“ћ≈Ќ»“№ редактирование или создание действи€
    /// </summary>
    public void Cancel()
    {
        bool inserted = this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Insert;
        this.DetailsViewActivitieDetails.ChangeMode(DetailsViewMode.ReadOnly);
        this.AddonInfo3.Cancel();

        if (inserted)
        {
            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
            if (this.HideActivitieDetails != null) this.HideActivitieDetails(this, new EventArgs());
        }
        else
        {
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// —ќ’–јЌ»“№ изменение или новое действие
    /// </summary>
    public void Post()
    {
        if (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Insert)
        {
            this.DetailsViewActivitieDetails.InsertItem(true);
            // тут выполнитс€ событие OnInserted и newActivitieID получит своЄ значение            
            this.AddonInfo3.Post(this.newActivitieID);
            this.newActivitieID = 0;

            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
            if (this.HideActivitieDetails != null) this.HideActivitieDetails(this, new EventArgs());
        }
        else if (this.DetailsViewActivitieDetails.CurrentMode == DetailsViewMode.Edit)
        {
            this.DetailsViewActivitieDetails.UpdateItem(true);
            this.AddonInfo3.Post(0);
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// показывыать контакты и задачи принадлежащие текущему клиенту
    /// </summary>
    /// <param name="clientID">клиент</param>
    protected void FilteredTaskAndContact(int clientID)
    {
        this.ObjectDataSourceDropDownTasks.SelectParameters.Clear();
        this.ObjectDataSourceDropDownTasks.SelectParameters.Add("ClientID", TypeCode.Int32, clientID.ToString());
        this.ObjectDataSourceDropDownTasks.DataBind();

        this.ObjectDataSourceDropDownContacts.SelectParameters.Clear();
        this.ObjectDataSourceDropDownContacts.SelectParameters.Add("ClientID", TypeCode.Int32, clientID.ToString());
        this.ObjectDataSourceDropDownContacts.DataBind();
    }

    /// <summary>
    /// событие на ѕ–»¬я« ” данных к дропдауну клиентов
    /// </summary>
    protected void DropDownListClientID_DataBound(object sender, EventArgs e)
    {
        this.DropDownListClientID_SelectedIndexChanged(sender, e);
    }

    /// <summary>
    /// событие на »«ћ≈Ќ≈Ќ»≈ дропдауна клиентов
    /// </summary>
    protected void DropDownListClientID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.IsAllCurrentZero)
        {
            //int clientID;
            //string strClientID = ((DropDownList)sender).SelectedValue;
            //if (int.TryParse(strClientID, out clientID))
            //    this.FilteredTaskAndContact(clientID);
        }
    }

}
