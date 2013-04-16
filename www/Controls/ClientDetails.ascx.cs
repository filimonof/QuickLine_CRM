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

public partial class Controls_ClientDetails : System.Web.UI.UserControl
{
    /// <summary>
    /// о каком клиенте выводим детальную информацию
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
    /// служет для узнавания ID клиента при добавлении нового
    /// </summary> 
    private int newClientID = 0;

    /// <summary>
    /// загрузка контрола ДЕТАЛИЗАЦИЯ КЛИЕНТА
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// привязка данных в контроле ДЕТАЛИЗАЦИЯ КЛИЕНТА
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceClientDetails.SelectParameters.Clear();
        this.ObjectDataSourceClientDetails.SelectParameters.Add("ID", TypeCode.Int32, this.BindClientID.ToString());
        this.ObjectDataSourceClientDetails.DataBind();
        this.DetailsViewClient.DataBind();

        this.AddonInfo1.RelationTable = TypeRelation.TR_CLIENT;
        this.AddonInfo1.X_ID = this.BindClientID;
        this.AddonInfo1.DataBind();
        this.AddonInfo1.ShowView();
    }

    /// <summary>
    /// обновление видимости кнопок редактирования
    /// </summary>
    private void UpdateButton()
    {
        this.ButtonEdit.Visible = this.DetailsViewClient.CurrentMode == DetailsViewMode.ReadOnly;
        this.ButtonDel.Visible = this.DetailsViewClient.CurrentMode == DetailsViewMode.ReadOnly;

        this.ButtonPost.Visible =
            (this.DetailsViewClient.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewClient.CurrentMode == DetailsViewMode.Edit);
        this.ButtonCancel.Visible =
            (this.DetailsViewClient.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewClient.CurrentMode == DetailsViewMode.Edit);
    }

    /// <summary>
    /// НОВЫЙЙ клиент
    /// </sumary>
    public void New()
    {
        if (this.BeforeInsert != null) this.BeforeInsert(this, new EventArgs());
        this.DetailsViewClient.ChangeMode(DetailsViewMode.Insert);

        this.AddonInfo1.RelationTable = TypeRelation.TR_CLIENT;
        this.AddonInfo1.New();
    }

    /// <summary>
    /// РЕДАКТИРОВАТЬ текущего клиента
    /// </summary>
    public void Edit()
    {
        if (this.BeforeUpdate != null) this.BeforeUpdate(this, new EventArgs());
        this.DetailsViewClient.ChangeMode(DetailsViewMode.Edit);
        this.AddonInfo1.Edit();
    }

    #region объявление событий

    /// <summary>
    /// событие - ВЫЙТИ из детализации
    /// </summary>
    public event EventHandler HideClientDetails;

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
    /// УДАЛИТЬ текущего клиента
    /// </summary>
    public void Del()
    {        
        this.DetailsViewClient.DeleteItem();
        this.AddonInfo1.Del();
        if (this.HideClientDetails != null) this.HideClientDetails(this, new EventArgs());
    }

    /// <summary>
    /// ОТМЕНИТЬ редактирование или создание клиента
    /// </summary>
    public void Cancel()
    {
        bool inserted = this.DetailsViewClient.CurrentMode == DetailsViewMode.Insert;
        this.DetailsViewClient.ChangeMode(DetailsViewMode.ReadOnly);
        this.AddonInfo1.Cancel();

        if (inserted)
        {
            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
            if (this.HideClientDetails != null) this.HideClientDetails(this, new EventArgs());
        }
        else
        {
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// СОХРАНИТЬ изменение или нового клиента
    /// </summary>
    public void Post()
    {
        if (this.DetailsViewClient.CurrentMode == DetailsViewMode.Insert)
        {
            this.DetailsViewClient.InsertItem(true);
            // тут выполнится событие OnInserted и newClientID получит своё значение            
            this.AddonInfo1.Post(this.newClientID);
            this.newClientID = 0;

            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
            if (this.HideClientDetails != null) this.HideClientDetails(this, new EventArgs());
        }
        else if (this.DetailsViewClient.CurrentMode == DetailsViewMode.Edit)
        {
            this.DetailsViewClient.UpdateItem(true);
            this.AddonInfo1.Post(0);
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// событие, ПЕРЕД тем как ДОБАВИЬ новый контакт
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceClientDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //текущая дата создания и модификации и агент сделавший это
        DateTime new_rec = DateTime.Now;
        e.InputParameters["CreationDateTime"] = new_rec;
        e.InputParameters["CreationAgentID"] = Session["CURRENT_AGENT"];
        e.InputParameters["LastModificationDateTime"] = new_rec;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
    }

    /// <summary>
    /// событие, ПЕРЕД РЕДАКТИРОВАНИЕ контакта
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceClientDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //текущая дата модификации и текущий агент (сделавший изменения)
        e.InputParameters["LastModificationDateTime"] = DateTime.Now;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
    }

    /// <summary>
    /// событие на добапвление клиента - узнаём ID нового клиента
    /// </summary>
    protected void ObjectDataSourceClientDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.newClientID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
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
