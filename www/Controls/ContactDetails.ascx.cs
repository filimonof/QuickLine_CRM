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

public partial class WebUserControls_ContactDetails : System.Web.UI.UserControl
{
    /// <summary>
    /// о каком контакте выводим детальную информацию
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
    /// текущий клиент, тот которому принадлежит данный контакт
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
    /// служет для узнавания ID контакта при добавлении нового
    /// </summary> 
    private int newContactID = 0;

    /// <summary>
    /// загрузка контрола ДЕТАЛИЗАЦИЯ КОНТАКТА
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// привязка данных в контроле ДЕТАЛИЗАЦИЯ КОНТАКТА
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceContactDetails.SelectParameters.Clear();
        this.ObjectDataSourceContactDetails.SelectParameters.Add("ID", TypeCode.Int32, this.BindContactID.ToString());
        this.ObjectDataSourceContactDetails.DataBind();
        this.DetailsViewContact.DataBind();

        this.AddonInfo2.RelationTable = TypeRelation.TR_CONTACT;
        this.AddonInfo2.X_ID = this.BindContactID;
        this.AddonInfo2.DataBind();
        this.AddonInfo2.ShowView();
    }

    /// <summary>
    /// обновление видимости кнопок редактирования
    /// </summary>
    private void UpdateButton()
    {
        this.ButtonEdit.Visible = this.DetailsViewContact.CurrentMode == DetailsViewMode.ReadOnly;
        this.ButtonDel.Visible = this.DetailsViewContact.CurrentMode == DetailsViewMode.ReadOnly;

        this.ButtonPost.Visible =
            (this.DetailsViewContact.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewContact.CurrentMode == DetailsViewMode.Edit);
        this.ButtonCancel.Visible =
            (this.DetailsViewContact.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewContact.CurrentMode == DetailsViewMode.Edit);
    }

    /// <summary>
    /// НОВЫЙЙ контакт
    /// </sumary>
    public void New()
    {
        if (this.BeforeInsert != null) this.BeforeInsert(this, new EventArgs());
        this.DetailsViewContact.ChangeMode(DetailsViewMode.Insert);

        this.AddonInfo2.RelationTable = TypeRelation.TR_CONTACT;
        this.AddonInfo2.New();
    }

    /// <summary>
    /// РЕДАКТИРОВАТЬ текущий контакт
    /// </summary>
    public void Edit()
    {
        if (this.BeforeUpdate != null) this.BeforeUpdate(this, new EventArgs());
        this.DetailsViewContact.ChangeMode(DetailsViewMode.Edit);
        this.AddonInfo2.Edit();
    }

    #region Объявление событий

    /// <summary>
    /// событие - ВЫЙТИ из детализации
    /// </summary>
    public event EventHandler HideContactDetails;

    /// <summary>
    /// событие - ПЕРЕД добавлением
    /// </summary>
    public event EventHandler BeforeInsert;
    /// <summary>
    /// событие - ПОСЛЕ добавления
    /// </summary>
    public event EventHandler AfterInsert;

    /// <summary>
    /// событие - ПЕРЕД редактированием
    /// </summary>
    public event EventHandler BeforeUpdate;
    /// <summary>
    /// событие - ПОСЛЕ редактированием
    /// </summary>
    public event EventHandler AfterUpdate;

    #endregion

    /// <summary>
    /// УДАЛИТЬ текущий контакт
    /// </summary>
    public void Del()
    {
        this.DetailsViewContact.DeleteItem();
        this.AddonInfo2.Del();
        if (this.HideContactDetails != null) this.HideContactDetails(this, new EventArgs());
    }

    /// <summary>
    /// ОТМЕНИТЬ редактирование или создание контакта
    /// </summary>
    public void Cancel()
    {
        bool inserted = this.DetailsViewContact.CurrentMode == DetailsViewMode.Insert;
        this.DetailsViewContact.ChangeMode(DetailsViewMode.ReadOnly);
        this.AddonInfo2.Cancel();

        if (inserted)
        {
            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
            if (this.HideContactDetails != null) this.HideContactDetails(this, new EventArgs());
        }
        else
        {
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// СОХРАНИТЬ изменение или нового контакта
    /// </summary>
    public void Post()
    {
        if (this.DetailsViewContact.CurrentMode == DetailsViewMode.Insert)
        {
            this.DetailsViewContact.InsertItem(true);
            // тут выполнится событие OnInserted и newContactID получит своё значение            
            this.AddonInfo2.Post(this.newContactID);
            this.newContactID = 0;

            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
            if (this.HideContactDetails != null) this.HideContactDetails(this, new EventArgs());
        }
        else if (this.DetailsViewContact.CurrentMode == DetailsViewMode.Edit)
        {
            this.DetailsViewContact.UpdateItem(true);
            this.AddonInfo2.Post(0);
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// событие, ПЕРЕД тем как ДОБАВИЬ новый контакт
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceContactDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //текущая дата создания и модификации и агент сделавший это
        DateTime new_rec = DateTime.Now;
        e.InputParameters["CreationDateTime"] = new_rec;
        e.InputParameters["CreationAgentID"] = Session["CURRENT_AGENT"];
        e.InputParameters["LastModificationDateTime"] = new_rec;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
        if (this.CurrentClientID != 0)
            e.InputParameters["ClientID"] = this.CurrentClientID;
    }

    /// <summary>
    /// событие, ПЕРЕД РЕДАКТИРОВАНИЕ контакта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceContactDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //текущая дата модификации и текущий агент (сделавший изменения)
        e.InputParameters["LastModificationDateTime"] = DateTime.Now;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
    }

    /// <summary>
    /// событие на добапвление клиента - узнаём ID нового клиента
    /// </summary>
    protected void ObjectDataSourceContactDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.newContactID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
    }

    #region Нажатия кнопок

    /// <summary>
    /// нажата кнопка РЕДАКТИРОВАНИ 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        this.Edit();
    }

    /// <summary>
    /// нажата кнопка ОТМЕНА 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        this.Cancel();
    }

    /// <summary>
    /// нажата кнопка УДАЛИТЬ 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonDel_Click(object sender, EventArgs e)
    {
        this.Del();
    }

    /// <summary>
    /// нажата кнопка СОХРАНИТЬ 
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

}


