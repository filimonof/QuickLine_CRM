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

public partial class Controls_ClientsList : System.Web.UI.UserControl
{

    /// <summary>
    /// клиент выбранный в списке клиентов (поле)
    /// </summary>
    private int selectedClientID = 0;

    /// <summary>
    /// клиент выбранный в списке клиентов
    /// </summary>
    public int SelectedClientID
    {
        get { return this.selectedClientID; }
    }

    /// <summary>
    /// загрузка контрола СПИСОК КЛИЕНТОВ
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// привязка данных в контроле СПИСКЕ КЛИЕНТОВ
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceClients.DataBind();
        this.GridViewClient.DataBind();
    }

    /// <summary>
    /// объявление события контрола - ВЫБОР КЛИЕНТА
    /// </summary>
    public event EventHandler ClientSelected;

    /// <summary>
    /// произошло событие, в списке клиентов выбран клиент
    /// </summary>
    protected void GridViewClient_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.selectedClientID = this.GridViewClient.SelectedValue != null ? (int)this.GridViewClient.SelectedValue : 0;
        // инициирование события - ВЫБОР КЛИЕНТА
        if (this.ClientSelected != null) this.ClientSelected(sender, e);
    }

}
