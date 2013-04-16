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

public partial class Controls_AddonInfoEdit : System.Web.UI.UserControl
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
                this.ObjectDataSourceAddonEdit.TypeName = "DataSet1TableAdapters.ClientInfo_SelectTableAdapter";
                break;
            case (TypeRelation.TR_CONTACT):
                this.ObjectDataSourceAddonEdit.TypeName = "DataSet1TableAdapters.ContactInfo_SelectTableAdapter";
                break;
            case (TypeRelation.TR_TASK):
                this.ObjectDataSourceAddonEdit.TypeName = "DataSet1TableAdapters.TaskInfo_SelectTableAdapter";
                break;
            case (TypeRelation.TR_ACTIVITIE):
                this.ObjectDataSourceAddonEdit.TypeName = "DataSet1TableAdapters.ActivitieInfo_SelectTableAdapter";
                break;
            default:
                //такого быть не должно
                break;
        }
        this.ObjectDataSourceAddonEdit.SelectParameters.Clear();
        this.ObjectDataSourceAddonEdit.SelectParameters.Add("X_ID", TypeCode.Int32, this.X_ID.ToString());
        this.ObjectDataSourceAddonEdit.DataBind();
        this.GridViewAddonEdit.DataBind();
    }

    /// <summary>
    /// событие - привязка данных к каждой строчке грида с редактируемой дополнительной инфой
    /// </summary>
    protected void GridViewAddonEdit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //нужен для биндинга Combo
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet1TableAdapters.InfoTypeComboTableAdapter adapterCombo = new DataSet1TableAdapters.InfoTypeComboTableAdapter();
            DropDownList ddownValueCombo = ((DropDownList)e.Row.FindControl("ValueCombo"));
            Literal litInfoTypeID = ((Literal)e.Row.FindControl("X_InfoTypeID"));
            TextBox tboxValueString = ((TextBox)e.Row.FindControl("ValueString"));
            Literal litTypeInfo = ((Literal)e.Row.FindControl("TypeInfo"));
            if (ddownValueCombo != null && litTypeInfo != null && litInfoTypeID != null && !litInfoTypeID.Text.Equals(string.Empty))
            {
                ddownValueCombo.Visible = String.Equals((string)litTypeInfo.Text, TypeInfo.TI_COMBO);
                ddownValueCombo.DataSource = adapterCombo.GetData(int.Parse(litInfoTypeID.Text));
                ddownValueCombo.DataValueField = "ID";
                ddownValueCombo.DataTextField = "Name";
                ddownValueCombo.DataBind();
                if (tboxValueString != null)
                    ddownValueCombo.SelectedValue = tboxValueString.Text;
            }
        }
    }

    /// <summary>
    /// очистить контролы редатирования
    /// </summary>
    public void Clear()
    {
        ClearRows(GridViewAddonEdit.Rows);
    }

    /// <summary>
    /// сохранить данные из контролов редактирования 
    /// </summary>
    /// <param name="newID">если новые данные то ID нового</param>
    public void SaveData(int newID)
    {
        //сохраняем дополнительные данные 
        foreach (GridViewRow row in this.GridViewAddonEdit.Rows)
        {
            string rowTypeInfo;
            Literal literalTypeInfo = (Literal)FindControlToRow("TypeInfo", row);
            if (literalTypeInfo == null) continue;
            rowTypeInfo = literalTypeInfo.Text;

            object rowValue = null;
            switch (rowTypeInfo)
            {
                case (TypeInfo.TI_STRING):
                    TextBox tbString = (TextBox)FindControlToRow("ValueString", row);
                    if (tbString != null) rowValue = tbString.Text;
                    break;
                case (TypeInfo.TI_INT):
                    TextBox tbInt = (TextBox)FindControlToRow("ValueInt", row);
                    if (tbInt != null && !tbInt.Text.Equals(string.Empty))
                    {
                        int res;
                        if (int.TryParse(tbInt.Text, out res))
                            rowValue = res;
                    }
                    break;
                case (TypeInfo.TI_BOOL):
                    CheckBox cbBool = (CheckBox)FindControlToRow("ValueBool", row);
                    if (cbBool != null) rowValue = cbBool.Checked;
                    break;
                case (TypeInfo.TI_COMBO):
                    DropDownList bblCombo = (DropDownList)FindControlToRow("ValueCombo", row);
                    if (bblCombo != null) rowValue = bblCombo.SelectedValue;
                    break;
                case (TypeInfo.TI_TEL):
                    TextBox tbTel = (TextBox)FindControlToRow("ValueTel", row);
                    if (tbTel != null) rowValue = tbTel.Text;
                    break;
            }

            int rowID = 0;
            Label literalID = (Label)FindControlToRow("ID", row);
            if (literalID != null && !literalID.Text.Equals(string.Empty)) rowID = int.Parse(literalID.Text);

            if (newID == 0)  // 0 - редактируем, другое значение - новые данные
            {
                Label literalClientID = (Label)FindControlToRow("X_ID", row);
                if (literalClientID != null) newID = int.Parse(literalClientID.Text);
            }

            int rowX_InfoTypeID = 0;
            Literal literalX_InfoTypeID = (Literal)FindControlToRow("X_InfoTypeID", row);
            if (literalX_InfoTypeID != null) rowX_InfoTypeID = int.Parse(literalX_InfoTypeID.Text);

            // а вот и само сохранение (сделано через update)

            switch (this.RelationTable)
            {
                case (TypeRelation.TR_CLIENT):
                    DataSet1TableAdapters.ClientInfo_SelectTableAdapter adapterClientInfo = new DataSet1TableAdapters.ClientInfo_SelectTableAdapter();
                    adapterClientInfo.Update(rowID, rowValue, newID, rowX_InfoTypeID);
                    break;
                case (TypeRelation.TR_CONTACT):
                    DataSet1TableAdapters.ContactInfo_SelectTableAdapter adapterContactInfo = new DataSet1TableAdapters.ContactInfo_SelectTableAdapter();
                    adapterContactInfo.Update(rowID, rowValue, newID, rowX_InfoTypeID);
                    break;
                case (TypeRelation.TR_TASK):
                    DataSet1TableAdapters.TaskInfo_SelectTableAdapter adapterTaskInfo = new DataSet1TableAdapters.TaskInfo_SelectTableAdapter();
                    adapterTaskInfo.Update(rowID, rowValue, newID, rowX_InfoTypeID);
                    break;
                case (TypeRelation.TR_ACTIVITIE):
                    DataSet1TableAdapters.ActivitieInfo_SelectTableAdapter adapterActivitieInfo = new DataSet1TableAdapters.ActivitieInfo_SelectTableAdapter();
                    adapterActivitieInfo.Update(rowID, rowValue, newID, rowX_InfoTypeID);
                    break;
                default:
                    // этого быть не должно
                    break;
            }
        }
    }

    /// <summary>
    ///  очистить грид rows 
    /// </summary>
    /// <param name="row">строчка грида</param>    
    private static void ClearRows(GridViewRowCollection rows)
    {
        foreach (GridViewRow row in rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in row.Cells)
                {
                    if (cell.Controls.Count > 0)
                    {
                        foreach (Control ctrl in cell.Controls)
                        {
                            if (ctrl != null)
                            {
                                if (ctrl is TextBox) ((TextBox)ctrl).Text = string.Empty;
                                //Literal - общая инфа о типе записи 
                                //Label - конкретно по текущей связи
                                //else if (ctrl is Literal) ((Literal)ctrl).Text = string.Empty;                                
                                else if (ctrl is Label) ((Label)ctrl).Text = string.Empty;
                                else if (ctrl is DropDownList) ((DropDownList)ctrl).SelectedIndex = -1;
                                else if (ctrl is CheckBox) ((CheckBox)ctrl).Checked = false;
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    ///  пробежать по строчке грида row и найти контрол с именем Name
    /// </summary>
    /// <param name="Name">имя контрола/поля в строке row грида </param>
    /// <param name="row">строчка грида</param>
    /// <returns>Control с ID=Name либо null</returns>
    private static Control FindControlToRow(string Name, GridViewRow row)
    {
        if (row.RowType == DataControlRowType.DataRow)
        {
            foreach (TableCell cell in row.Cells)
            {
                if (cell.Controls.Count > 0)
                {
                    Control ctrl = cell.FindControl(Name);
                    if (ctrl != null)
                        return ctrl;
                }
            }
        }
        return null;
    }

}
