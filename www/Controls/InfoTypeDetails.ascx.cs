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

public partial class Controls_InfoTypeDetails : System.Web.UI.UserControl
{
    /// <summary>
    /// о каком типе информации выводим детальную информацию
    /// </summary>   
    public int BindTypeInfoID
    {
        get
        {
            object obj = ViewState["TYPEINFO_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["TYPEINFO_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// служет для узнавания ID типа при добавлении нового
    /// </summary>
    private int newInfoTypeID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// привязка данных в контроле ДЕТАЛИЗАЦИЯ ТИПОВ ИНФОРМАЦИИ
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceInfoTypeToID.SelectParameters.Clear();
        this.ObjectDataSourceInfoTypeToID.SelectParameters.Add("ID", TypeCode.Int32, this.BindTypeInfoID.ToString());
        this.ObjectDataSourceInfoTypeToID.DataBind();
        this.DetailsViewTypeInfo.DataBind();

        this.InfoTypeCombo1.BindTypeInfoID = this.BindTypeInfoID;
        this.InfoTypeCombo1.DataBind();
    }

    /// <summary>
    /// НОВЫЙ ТИП ИНФОРМАЦИИ
    /// </sumary>
    public void New()
    {
        if (this.BeforeInsert != null) this.BeforeInsert(this, new EventArgs());
        this.DetailsViewTypeInfo.ChangeMode(DetailsViewMode.Insert);
    }

    /// <summary>
    /// РЕДАКТИРОВАТЬ текущую задачу
    /// </summary>
    public void Edit()
    {
        if (this.BeforeUpdate != null) this.BeforeUpdate(this, new EventArgs());
        this.DetailsViewTypeInfo.ChangeMode(DetailsViewMode.Edit);
    }

    #region Объявление событий

    /// <summary>
    /// событие - ВЫЙТИ из детализации
    /// </summary>
    public event EventHandler HideInfoTypeDetails;

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
        this.DetailsViewTypeInfo.ChangeMode(DetailsViewMode.ReadOnly);
        this.DetailsViewTypeInfo.DeleteItem();
        this.InfoTypeCombo1.Del();
        //ВАЖНО при таком переходе возможны ошибки загрузки состояния
        if (this.HideInfoTypeDetails != null) this.HideInfoTypeDetails(this, new EventArgs());
    }

    /// <summary>
    /// ОТМЕНИТЬ редактирование или создание задачи
    /// </summary>
    public void Cancel()
    {
        bool inserted = this.DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Insert;
        this.DetailsViewTypeInfo.ChangeMode(DetailsViewMode.ReadOnly);
        this.InfoTypeCombo1.Del();
        if (inserted)
        {
            if (this.AfterInsert != null) this.AfterInsert(this, new EventArgs());
        }
        else
        {
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }

        //ВАЖНО при таком переходе возможны ошибки загрузки состояния
        if (this.HideInfoTypeDetails != null) this.HideInfoTypeDetails(this, new EventArgs());        
    }

    /// <summary>
    /// СОХРАНИТЬ изменение или новую задачу
    /// </summary>
    public void Post()
    {
        if (this.DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Insert)
        {
            this.DetailsViewTypeInfo.InsertItem(true);
            // тут выполнится событие OnInserted и newTaskID получит своё значение     
            this.InfoTypeCombo1.SaveNew(this.newInfoTypeID);
            this.newInfoTypeID = 0;
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }
        else if (this.DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Edit)
        {
            this.DetailsViewTypeInfo.UpdateItem(true);
            this.InfoTypeCombo1.SaveEdit();
            if (this.AfterUpdate != null) this.AfterUpdate(this, new EventArgs());
        }

        //this.InfoTypeCombo1.DataBind();

        //ВАЖНО при таком переходе возможны ошибки загрузки состояния
        if (this.HideInfoTypeDetails != null) this.HideInfoTypeDetails(this, new EventArgs());
    }

    #region Нажатия на кнопки редактирования данных

    protected void ButtonPost_Click(object sender, EventArgs e)
    {
        this.Post();
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        this.Cancel();
    }
    protected void ButtonDel_Click(object sender, EventArgs e)
    {
        this.Del();
    }
    protected void ButtonDel_PreRender(object sender, EventArgs e)
    {
        this.ButtonDel.Visible = this.DetailsViewTypeInfo.CurrentMode != DetailsViewMode.Insert;
    }

    #endregion

    /// <summary>
    /// событие на добапвление типа - узнаём ID нового типа
    /// </summary>
    protected void ObjectDataSourceInfoTypeToID_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.newInfoTypeID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
    }

    /// <summary>
    /// событие на ИЗМЕНЕНИЕ типа данных при редактировании
    /// </summary>
    protected void DropDownListTypeInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //todo: можно сделать чтобы при изменении временно введённые данные тоже сохранялись, удобно ли?
        if (((DropDownList)sender).SelectedValue == TypeInfo.TI_COMBO)
            this.InfoTypeCombo1.Show(this.DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Insert);
        else
            this.InfoTypeCombo1.Hide();
    }

    /// <summary>
    /// событие на ПРИВЯЗКУ данных к комбобоксу с типом данных
    /// </summary>
    protected void DropDownListTypeInfo_DataBound(object sender, EventArgs e)
    {
        this.DropDownListTypeInfo_SelectedIndexChanged(sender, e);
    }

}
