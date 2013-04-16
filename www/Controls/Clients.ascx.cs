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

public partial class Controls_Clients : System.Web.UI.UserControl
{
    /// <summary>
    /// загрузка страницы
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // тут должна быть ваша реклама
        if (!IsPostBack) this.DataBind();
    }

    /// <summary>
    /// привязка данных в контроле КЛИЕНТЫ
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();                
        this.ClientsList1.DataBind();
    }

    /// <summary>
    /// произошло событие, выбор клиента для просмотра в контрле ДЕТАЛИЗАЦИЯ КЛИЕНТОВ
    /// </summary>    
    protected void ClientsList_ClientSelected(object sender, EventArgs e)
    {
        this.ClientDetails1.BindClientID = this.ClientsList1.SelectedClientID;
        this.ClientDetails1.DataBind();

        //выводим контакты выбранного клиента
        this.Contacts1.BindClientID = this.ClientsList1.SelectedClientID;
        this.Contacts1.DataBind();
        this.Contacts1.ShowList();

        //выводим задачи выбранного клиента
        this.Tasks1.BindClientID = this.ClientsList1.SelectedClientID;
        this.Tasks1.DataBind();
        this.Tasks1.ShowList();

        //выводим действия выбранного клиента
        this.Activities3.BindContactID = 0;
        this.Activities3.BindTaskID = 0;
        this.Activities3.BindClientID = this.ClientsList1.SelectedClientID; ;
        this.Activities3.DataBind();
        this.Activities3.ShowList();

        this.ShowDetail();
        this.TabsTreeC1.ShowDetailTab();
    }

    /// <summary>
    /// переключиться на СПИСОК КЛИЕНТОВ
    /// </summary>
    public void ShowList()
    {
        this.MultiViewClients.ActiveViewIndex = 0;
        this.TabsTreeC1.ShowListingTab();
    }

    /// <summary>
    /// переключиться на ДЕТАЛИЗАЦИЮ КЛИЕНТОВ
    /// </summary>
    public void ShowDetail()
    {
        this.MultiViewClients.ActiveViewIndex = 1;
    }

    /// <summary>
    /// событие - нажата вкладка ПОКАЗАТЬ СПИСОК КЛИЕНТОВ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeC1_ClickListing(object sender, EventArgs e)
    {
        this.ClientDetails1.Cancel();
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// произошло событие - выбор вкладки СОЗДАТЬ КЛИЕНТА
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TabsTreeC1_ClickNew(object sender, EventArgs e)
    {
        this.ClientDetails1.New();
        this.ShowDetail();
        this.TabsTreeC1.ShowNewTab();
    }

    /// <summary>
    /// произошло событие, ДЕТАЛИЗАЦИЯ КЛИЕНТОВ изъявило желание переключиться на список контактов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ClientDetails1_HideClientDetails(object sender, EventArgs e)
    {
        this.DataBind();
        this.ShowList();
    }

    /// <summary>
    /// произошло событие перед добавлением в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ClientDetails1_BeforeInsert(object sender, EventArgs e)
    {
        this.Contacts1.Visible = false;
        this.Tasks1.Visible = false;
        this.Activities3.Visible = false;
    }

    /// <summary>
    /// произошло событие после добавления в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ClientDetails1_AfterInsert(object sender, EventArgs e)
    {
        this.Contacts1.Visible = true;
        this.Tasks1.Visible = true;
        this.Activities3.Visible = true;
    }

    /// <summary>
    /// произошло событие перед редактированием в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ClientDetails1_BeforeUpdate(object sender, EventArgs e)
    {
        this.TabsTreeC1.UnclickedListingTab(false);
    }

    /// <summary>
    /// произошло событие после редактирования в детализации
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ClientDetails1_AfterUpdate(object sender, EventArgs e)
    {
        this.TabsTreeC1.UnclickedListingTab(true);
    }

}
