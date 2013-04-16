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

public partial class Controls_InfoTypes : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.DataBind();
    }

    /// <summary>
    /// привязка данных в контроле ТИПЫ ИНФОРМАЦИИ
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();                
        this.InfoTypesList1.DataBind();
    }

    /// <summary>
    /// произошло событие, выбор типа информации для просмотра в контрле ДЕТАЛИЗАЦИЯ ТИПОВ ИНФОРМАЦИИ
    /// </summary>    
    protected void InfoTypesList1_TypeInfoSelected(object sender, EventArgs e)
    {
        this.InfoTypeDetails1.BindTypeInfoID = this.InfoTypesList1.SelectedTypeInfoID;
        this.InfoTypeDetails1.DataBind();

        this.ShowDetail();
        this.TabsTreeIT1.ShowDetailTab();

        //сразу переходим в режим редактирования
        this.InfoTypeDetails1.Edit();
    }

    /// <summary>
    /// переключиться на СПИСОК ТИПОВ ИНФОРМАЦИИ
    /// </summary>
    public void ShowList()
    {
        this.MultiViewInfoType.ActiveViewIndex = 0;
        this.TabsTreeIT1.ShowListingTab();
    }

    /// <summary>
    /// переключиться на ДЕТАЛИЗАЦИЮ ТИПА ИНОФРМАЦИИ
    /// </summary>
    public void ShowDetail()
    {
        this.MultiViewInfoType.ActiveViewIndex = 1;
    }

    /// <summary>
    /// событие - нажата вкладка ПОКАЗАТЬ СПИСОК ТИПОВ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeIT1_ClickListing(object sender, EventArgs e)
    {
        //по логиике невозможно будет нажать на "список типов информации"
        this.InfoTypeDetails1.Cancel();
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// произошло событие - выбор вкладки СОЗДАТЬ ТИП ИНФОРМАЦИИ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeIT1_ClickNew(object sender, EventArgs e)
    {
        this.InfoTypeDetails1.New();
        this.ShowDetail();
        this.TabsTreeIT1.ShowNewTab();
    }

    /// <summary>
    /// произошло событие, ДЕТАЛИЗАЦИЯ ТИПА ИНФОРМАЦИИ изъявило желание переключиться на список типов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InfoTypeDetails1_HideInfoTypeDetails(object sender, EventArgs e)
    {
        this.DataBind();
        // изза этого ShowList вылазит ошибка состояния НУЖНО РАЗОБРАТЬСЯ
        this.ShowList();
    }

    /// <summary>
    /// произошло событие перед редактированием в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InfoTypeDetails1_BeforeUpdate(object sender, EventArgs e)
    {
        this.TabsTreeIT1.UnclickedListingTab(false);
    }

    /// <summary>
    /// произошло событие после редактирования в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InfoTypeDetails1_AfterUpdate(object sender, EventArgs e)
    {
        this.TabsTreeIT1.UnclickedListingTab(true);
    }



}
