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

public partial class WebUserControls_ContactsList : System.Web.UI.UserControl
{
    /// <summary>
    /// какому клиенту принадлежит данный контрол СПИСОК КОНТАКТОВ, 
    /// если 0 выводим все контакты
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
    /// контакт выбранный в списке контактов (поле)
    /// </summary>
    private int selectedContactID = 0;

    /// <summary>
    /// контакт выбранный в списке контактов
    /// </summary>
    public int SelectedContactID
    {
        get { return this.selectedContactID; }        
    }

    /// <summary>
    /// загрузка контрола СПИСОК КОНТАКТОВ
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack) this.DataBind();        
    }

    /// <summary>
    /// привязка данных в контроле СПИСКЕ КОНТАКТОВ
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceContacts.SelectParameters.Clear();
        this.ObjectDataSourceContacts.SelectParameters.Add("ClientID", TypeCode.Int32, this.BindClientID.ToString());
        this.ObjectDataSourceContacts.DataBind();
        this.GridViewContacts.DataBind();
    }

    /// <summary>
    /// объявление события контрола - ВЫБОР КОНТАКТА
    /// </summary>
    public event EventHandler ContactSelected;

    /// <summary>
    /// произошло событие, в списке контактов выбран контакт
    /// </summary>
    protected void GridViewContacts_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.selectedContactID = this.GridViewContacts.SelectedValue != null ? (int)this.GridViewContacts.SelectedValue : 0;
        // инициирование события - ВЫБОР КОНТАКТА
        if (this.ContactSelected != null) this.ContactSelected(sender, e);
    }
}
