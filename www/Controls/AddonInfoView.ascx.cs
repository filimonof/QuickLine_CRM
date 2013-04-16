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

public partial class Controls_AddonInfoView : System.Web.UI.UserControl
{

    /// <summary>
    /// с какими данными работаем (client, contact, task, activitie)
    /// </summary>
    public string RelationTable
    {
        get
        {
            object obj = ViewState["RELATION_TABLE" + this.UniqueID];
            return obj != null ? obj.ToString() : string.Empty;
        }
        set
        {
            ViewState["RELATION_TABLE" + this.UniqueID] = value;
        }
    }

    /// <summary>
    /// ID данных (ClientID, ContactID, TaskID, ActivitieID )
    /// </summary>
    public int X_ID
    {
        get
        {
            object obj = ViewState["X_ID" + this.UniqueID];
            return obj != null ? (int)obj : 0;
        }
        set
        {
            ViewState["X_ID" + this.UniqueID] = value < 0 ? 0 : value;
        }
    }

    /// <summary>
    /// загрузка страницы
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// привязка данных
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        switch (this.RelationTable)
        {
            case (TypeRelation.TR_CLIENT):
                this.ObjectDataSourceAddon.TypeName = "DataSet1TableAdapters.ClientInfo_SelectTableAdapter";
                break;
            case (TypeRelation.TR_CONTACT):
                this.ObjectDataSourceAddon.TypeName = "DataSet1TableAdapters.ContactInfo_SelectTableAdapter";
                break;
            case (TypeRelation.TR_TASK):
                this.ObjectDataSourceAddon.TypeName = "DataSet1TableAdapters.TaskInfo_SelectTableAdapter";
                break;
            case (TypeRelation.TR_ACTIVITIE):
                this.ObjectDataSourceAddon.TypeName = "DataSet1TableAdapters.ActivitieInfo_SelectTableAdapter";
                break;
            default:
                // этого не может быть, промежуток должен быть
                break;
        }
        this.ObjectDataSourceAddon.SelectParameters.Clear();
        this.ObjectDataSourceAddon.SelectParameters.Add("X_ID", TypeCode.Int32, this.X_ID.ToString());
        this.ObjectDataSourceAddon.DataBind();
        this.GridViewAddonList.DataBind();
    }

    /// <summary>
    /// событие - привязка данных к каждой строчке грида с просмотром дополнительной инфы
    /// </summary>
    protected void GridViewAddonList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //в этом столбце грида информация и типе
        const int CELL_TYPE_INFO = 2; 

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[CELL_TYPE_INFO].Text == TypeInfo.TI_COMBO)
            {
                Label lbCombo = ((Label)e.Row.FindControl("ValueComboList"));
                if (lbCombo != null)
                {
                    DataSet1TableAdapters.InfoTypeComboTableAdapter adapterCombo = new DataSet1TableAdapters.InfoTypeComboTableAdapter();
                    int res;
                    if (int.TryParse(lbCombo.Text, out res))
                        lbCombo.Text = (string)adapterCombo.GetNameToID(res);
                    else
                        lbCombo.Text = string.Empty;
                }
            }
            e.Row.Cells[CELL_TYPE_INFO].Visible = false;
        }
    }
}
