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

public partial class InfoType : System.Web.UI.Page
{

    /// <summary>
    /// служет дл€ узнавани€ ID типа при добавлении нового
    /// </summary>
    private int NewInfoTypeID = 0;

    /// <summary>
    /// статическа€ переменна€ дл€ хранени€ таблицы с комбобоксом
    /// </summary>
    private static DataTable TableCombo;


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// кнопка —ќ«ƒј“№ новый тип
    /// </summary>
    protected void ButtonAddType_Click(object sender, EventArgs e)
    {
        DetailsViewTypeInfo.ChangeMode(DetailsViewMode.Insert);
        MultiViewInfoType.ActiveViewIndex = 1;
    }

    /// <summary>
    /// кнопка —ќ’–јЌ»“№ изменени€
    /// </summary>
    protected void ButtonPost_Click(object sender, EventArgs e)
    {
        // добавл€ем / измен€ем типы        
        if (DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Insert)
        {
            DetailsViewTypeInfo.InsertItem(true);
            // тут через событие ловим this.NewInfoTypeID

            SaveRepeaterToTableCombo();
            if (this.NewInfoTypeID != 0)
                SaveTableComboToDatabase(this.NewInfoTypeID);
            this.NewInfoTypeID = 0;
        }
        else if (DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Edit)
        {
            DetailsViewTypeInfo.UpdateItem(true);

            SaveRepeaterToTableCombo();
            DeleteErasedRowToDatabase();
            SaveTableComboToDatabase((int)DetailsViewTypeInfo.SelectedValue);
        }

        RepeaterCombo.DataSource = TableCombo;
        RepeaterCombo.DataBind();
        GridViewList.DataBind();
        DetailsViewTypeInfo.DataBind();

        MultiViewInfoType.ActiveViewIndex = 0;
    }

    /// <summary>
    /// событие на добапвление типа - узнаЄм ID нового типа
    /// </summary>
    protected void ObjectDataSourceInfoTypeToID_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.NewInfoTypeID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
    }

    /// <summary>
    /// кнопка ќ“ћ≈Ќ»“№ изменени€
    /// </summary>
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        DetailsViewTypeInfo.ChangeMode(DetailsViewMode.ReadOnly);

        DeleteTableCombo();

        RepeaterCombo.DataSource = null;
        RepeaterCombo.DataBind();
        GridViewList.DataBind();
        DetailsViewTypeInfo.DataBind();

        MultiViewInfoType.ActiveViewIndex = 0;
    }

    /// <summary>
    /// кнопка ”ƒјЋ»“№
    /// </summary>
    protected void ButtonDel_Click(object sender, EventArgs e)
    {
        //todo: можно проврить св€зи по InfoTypeID (Combo) и помен€ть значение на другое

        DetailsViewTypeInfo.ChangeMode(DetailsViewMode.ReadOnly);
        DetailsViewTypeInfo.DeleteItem();

        DeleteTableCombo();

        RepeaterCombo.DataSource = null;
        RepeaterCombo.DataBind();
        GridViewList.DataBind();
        DetailsViewTypeInfo.DataBind();

        MultiViewInfoType.ActiveViewIndex = 0;
    }

    /// <summary>
    /// кнопка ѕ–ќ—ћќ“– информации - выполн€ет роль редактировани€ информации
    /// </summary>
    protected void GridViewList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //инфу в режим редактировани€ и переключитьс€ на неЄ 
        DetailsViewTypeInfo.ChangeMode(DetailsViewMode.Edit);

        MultiViewInfoType.ActiveViewIndex = 1;
    }

    /// <summary>
    /// —ќ«ƒј®ћ таблицу дл€  омбоЅокса
    /// </summary>
    /// <param name="IsNew">true если новые данные</param>
    public void InitCombo(bool IsNew)
    {
        if (TableCombo != null) DeleteTableCombo();

        TableCombo = new DataTable("tblCombo");
        TableCombo.Columns.Add(new DataColumn("ID", Type.GetType("System.Int32")));
        TableCombo.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
        TableCombo.Columns.Add(new DataColumn("NumSorted", Type.GetType("System.Int32")));
        TableCombo.Columns["ID"].AllowDBNull = false;
        TableCombo.Columns["ID"].DefaultValue = 0;
        TableCombo.Columns["Name"].AllowDBNull = true;
        TableCombo.Columns["Name"].DefaultValue = "";
        TableCombo.Columns["NumSorted"].AllowDBNull = false;
        TableCombo.Columns["NumSorted"].DefaultValue = "100";

        if (!IsNew)
        {
            // загружаем данные из реальной бызы
            DataSet1TableAdapters.InfoTypeComboTableAdapter adapterCombo = new DataSet1TableAdapters.InfoTypeComboTableAdapter();
            DataSet1.InfoTypeComboDataTable dataTableCombo = adapterCombo.GetData((int)DetailsViewTypeInfo.SelectedValue);

            if (dataTableCombo.Rows.Count > 0)
            {
                foreach (DataSet1.InfoTypeComboRow rowCombo in dataTableCombo.Rows)
                {
                    DataRow row;
                    row = TableCombo.NewRow();
                    row["ID"] = rowCombo["ID"];
                    row["Name"] = rowCombo["Name"];
                    row["NumSorted"] = rowCombo["NumSorted"];
                    TableCombo.Rows.Add(row);
                }
            }
        }
    }

    /// <summary>
    /// событие на »«ћ≈Ќ≈Ќ»≈ типа данных при редактировании
    /// </summary>
    protected void DropDownListTypeInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //todo: можно сделать чтобы при изменении временно введЄнные данные тоже сохран€лись, удобно ли?
        if (((DropDownList)sender).SelectedValue == "combo")
        {
            LabelHeaderCombo.Visible = true;
            LabelAddCombo.Visible = true;
            LabelAddComboNum.Visible = true;
            TextBoxAddCombo.Visible = true;
            TextBoxAddComboNum.Visible = true;
            ButtonAddCombo.Visible = true;

            //todo: если InfoTypeID тот же и таблица tblCombo существует то инициализировать не нада

            InitCombo(DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Insert);
            RepeaterCombo.DataSourceID = null;
            RepeaterCombo.DataSource = TableCombo;
        }
        else
        {
            LabelHeaderCombo.Visible = false;
            LabelAddCombo.Visible = false;
            LabelAddComboNum.Visible = false;
            TextBoxAddCombo.Visible = false;
            TextBoxAddComboNum.Visible = false;
            ButtonAddCombo.Visible = false;

            RepeaterCombo.DataSource = null;
        }
        RepeaterCombo.DataBind();
    }

    /// <summary>
    /// событие на ѕ–»¬я« ” данных к комбобоксу с типом данных
    /// </summary>
    protected void DropDownListTypeInfo_DataBound(object sender, EventArgs e)
    {
        DropDownListTypeInfo_SelectedIndexChanged(sender, e);
    }

    /// <summary>
    /// ”ƒјЋ»“№ строку в таблице дл€ комбобокса
    /// </summary>
    protected void ButtonDelCombo_Click(object sender, EventArgs e)
    {
        //todo: проверить св€зи при удалении
        // что будет если ID удал€емой уже где то используетс€ - а там будет пусто
        if (TableCombo != null)
        {
            RepeaterItem it = (RepeaterItem)((Button)sender).Parent;
            TableCombo.Rows.RemoveAt(it.ItemIndex);
            RepeaterCombo.DataSource = TableCombo;
            RepeaterCombo.DataBind();
        }
    }

    /// <summary>
    /// ƒќЅј¬»“№ строку в  омбоЅоксе
    /// </summary>
    protected void ButtonAddCombo_Click(object sender, EventArgs e)
    {
        //сохраним отредактированые на экране данные
        SaveRepeaterToTableCombo();

        if (TableCombo != null)
        {
            DataRow row;
            row = TableCombo.NewRow();
            row["Name"] = TextBoxAddCombo.Text;
            int res;
            if (int.TryParse(TextBoxAddComboNum.Text, out res))
                row["NumSorted"] = res;
            else
                row["NumSorted"] = row["ID"];
            TableCombo.Rows.Add(row);

            RepeaterCombo.DataSource = TableCombo;
            RepeaterCombo.DataBind();

            TextBoxAddCombo.Text = "";
            TextBoxAddComboNum.Text = "";
        }
    }

    /// <summary>
    /// удалить таблицу дл€ Combo
    /// </summary>
    private void DeleteTableCombo()
    {
        if (TableCombo != null)
        {
            TableCombo.Dispose();
            TableCombo = null;
        }
    }

    /// <summary>
    /// сохранить данные с экрана во временную таблицу tblCombo
    /// </summary>
    private void SaveRepeaterToTableCombo()
    {
        if (TableCombo != null)
        {
            foreach (RepeaterItem ri in RepeaterCombo.Items)
            {
                TextBox tbCombo = (TextBox)ri.FindControl("TextBoxCombo");
                if (tbCombo != null)
                    TableCombo.Rows[ri.ItemIndex]["Name"] = tbCombo.Text;

                TextBox tbComboNum = (TextBox)ri.FindControl("TextBoxComboNum");
                int num;
                if (tbComboNum != null && int.TryParse(tbComboNum.Text, out num))
                    TableCombo.Rows[ri.ItemIndex]["NumSorted"] = num;
            }
        }
    }

    /// <summary>
    /// сохранить временную таблицу tblCombo в Ѕазу данных
    /// </summary>
    private void SaveTableComboToDatabase(int saveInfoTypeID)
    {
        if (TableCombo != null)
        {
            //можно сохран€ть только если тип combo, но сохраню ка € всегда, хуже не будет!

            DataSet1TableAdapters.InfoTypeComboTableAdapter adapterCombo = new DataSet1TableAdapters.InfoTypeComboTableAdapter();

            foreach (DataRow row in TableCombo.Rows)
            {
                adapterCombo.SaveRow((int)row["ID"], (string)row["Name"], saveInfoTypeID, (int)row["NumSorted"]);
            }
        }
    }

    /// <summary>
    /// удалить из реальной базы строки удалЄнные во временной tblCombo
    /// </summary>
    private void DeleteErasedRowToDatabase()
    {
        if (TableCombo != null)
        {
            // берЄм дл€ сравнени€ данные из реальной бызы
            DataSet1TableAdapters.InfoTypeComboTableAdapter adapterCombo = new DataSet1TableAdapters.InfoTypeComboTableAdapter();
            DataSet1.InfoTypeComboDataTable dataTableCombo = adapterCombo.GetData((int)DetailsViewTypeInfo.SelectedValue);

            if (dataTableCombo.Rows.Count > 0)
            {
                foreach (DataSet1.InfoTypeComboRow rowCombo in dataTableCombo.Rows)
                {
                    //если строки в tblCombo нету то удалить, если естть то ничего не делаем
                    DataRow[] dRow = TableCombo.Select(" ID = " + rowCombo["ID"].ToString());
                    if (dRow.Length == 0)
                        adapterCombo.Delete((int)rowCombo["ID"]);
                }
            }

        }
    }

}
