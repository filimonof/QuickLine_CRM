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

public partial class Controls_InfoTypesList : System.Web.UI.UserControl
{
    /// <summary>
    /// тип выбранный в списке типов (поле)
    /// </summary>
    private int selectedTypeInfoID = 0;

    /// <summary>
    /// тип выбранный в списке типов
    /// </summary>
    public int SelectedTypeInfoID
    {
        get { return this.selectedTypeInfoID; }        
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// привязка данных
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceInfoType.DataBind();
        this.GridViewInfoTypesList.DataBind();
    }

    /// <summary>
    /// объявление события контрола - ВЫБОР ТИПА
    /// </summary>
    public event EventHandler TypeInfoSelected;

    /// <summary>
    /// произошло событие, в списке типов информации выбран тип
    /// </summary>
    protected void GridViewInfoTypesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.selectedTypeInfoID = this.GridViewInfoTypesList.SelectedValue != null ? (int)this.GridViewInfoTypesList.SelectedValue : 0;
        // инициирование события - ВЫБОР КОНТАКТА
        if (this.TypeInfoSelected != null) this.TypeInfoSelected(sender, e);
    }
}
