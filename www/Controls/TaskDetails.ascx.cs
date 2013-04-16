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

public partial class Controls_TaskDetails : System.Web.UI.UserControl
{
    /// <summary>
    /// о какой задаче выводим детальную информацию
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
    /// текущий клиент, тот которому принадлежит данная задача
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
    /// служет для узнавания ID задачи при добавлении нового
    /// </summary> 
    private int newTaskID = 0;

    /// <summary>
    /// загрузка контрола ДЕТАЛИЗАЦИЯ ЗАДАЧИ
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// привязка данных в контроле ДЕТАЛИЗАЦИЯ ЗАДАЧИ
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceTaskDetails.SelectParameters.Clear();
        this.ObjectDataSourceTaskDetails.SelectParameters.Add("ID", TypeCode.Int32, this.BindTaskID.ToString());
        this.ObjectDataSourceTaskDetails.DataBind();
        this.DetailsViewTask.DataBind();

        this.AddonInfo4.RelationTable = TypeRelation.TR_TASK;
        this.AddonInfo4.X_ID = this.BindTaskID;
        this.AddonInfo4.DataBind();
        this.AddonInfo4.ShowView();
    }

    /// <summary>
    /// обновление видимости кнопок редактирования
    /// </summary>
    private void UpdateButton()
    {
        this.ButtonEdit.Visible = this.DetailsViewTask.CurrentMode == DetailsViewMode.ReadOnly;
        this.ButtonDel.Visible = this.DetailsViewTask.CurrentMode == DetailsViewMode.ReadOnly;

        this.ButtonPost.Visible =
            (this.DetailsViewTask.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewTask.CurrentMode == DetailsViewMode.Edit);
        this.ButtonCancel.Visible =
            (this.DetailsViewTask.CurrentMode == DetailsViewMode.Insert)
            || (this.DetailsViewTask.CurrentMode == DetailsViewMode.Edit);
    }

    /// <summary>
    /// НОВАЯ ЗАДАЧА
    /// </sumary>
    public void New()
    {
        if (this.BeforeInsert != null) this.BeforeInsert(this, new EventArgs());
        this.DetailsViewTask.ChangeMode(DetailsViewMode.Insert);

        this.AddonInfo4.RelationTable = TypeRelation.TR_TASK;
        this.AddonInfo4.New();
    }

    /// <summary>
    /// РЕДАКТИРОВАТЬ текущую задачу
    /// </summary>
    public void Edit()
    {
        if (this.BeforeUpdate != null) this.BeforeUpdate(this, new EventArgs());
        this.DetailsViewTask.ChangeMode(DetailsViewMode.Edit);
        this.AddonInfo4.Edit();
    }

    #region Объявление событий

    /// <summary>
    /// событие - ВЫЙТИ из детализации
    /// </summary>
    public event EventHandler HideTaskDetails;

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
    /// УДАЛИТЬ текущую задачу
    /// </summary>
    public void Del()
    {
        this.DetailsViewTask.DeleteItem();
        this.AddonInfo4.Del();
        if (this.HideTaskDetails != null) this.HideTaskDetails(this, new EventArgs());
    }

    /// <summary>
    /// ОТМЕНИТЬ редактирование или создание задачи
    /// </summary>
    public void Cancel()
    {
        bool inserted = this.DetailsViewTask.CurrentMode == DetailsViewMode.Insert;
        this.DetailsViewTask.ChangeMode(DetailsViewMode.ReadOnly);
        this.AddonInfo4.Cancel();

        if (inserted)
        {
            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
            if (this.HideTaskDetails != null) this.HideTaskDetails(this, new EventArgs());
        }
        else
        {
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// СОХРАНИТЬ изменение или новую задачу
    /// </summary>
    public void Post()
    {
        if (this.DetailsViewTask.CurrentMode == DetailsViewMode.Insert)
        {
            this.DetailsViewTask.InsertItem(true);
            // тут выполнится событие OnInserted и newTaskID получит своё значение            
            this.AddonInfo4.Post(this.newTaskID);
            this.newTaskID = 0;

            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
            if (this.HideTaskDetails != null) this.HideTaskDetails(this, new EventArgs());
        }
        else if (this.DetailsViewTask.CurrentMode == DetailsViewMode.Edit)
        {
            this.DetailsViewTask.UpdateItem(true);
            this.AddonInfo4.Post(0);
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
    }

    /// <summary>
    /// событие, ПЕРЕД тем как ДОБАВИЬ новую задачу
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceTaskDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
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
    /// событие, ПЕРЕД РЕДАКТИРОВАНИЕМ ЗАДАЧ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ObjectDataSourceTaskDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //текущая дата модификации и текущий агент (сделавший изменения)
        e.InputParameters["LastModificationDateTime"] = DateTime.Now;
        e.InputParameters["LastModificationAgentID"] = Session["CURRENT_AGENT"];
    }

    /// <summary>
    /// событие на добапвление задачи - узнаём ID новой задачи
    /// </summary>
    protected void ObjectDataSourceTaskDetails_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.newTaskID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
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
