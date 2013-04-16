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

public partial class Controls_InfoTypeCombo : System.Web.UI.UserControl
{

    /// <summary>
    /// какому типу информации принадлежит этот Combo
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
    /// статическая переменная для хранения таблицы с комбобоксом
    /// </summary>
    private static DataTable TableCombo = null;

    /// <summary>
    /// загрузка страницы
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// привязка данных в контроле ДЕТАЛИЗАЦИЯ ТИПОВ ИНФОРМАЦИИ
    /// </summary>
    public override void DataBind()
    {
        //base.DataBind();
        this.ObjectDataSourceCombo.SelectParameters.Clear();
        this.ObjectDataSourceCombo.SelectParameters.Add("InfoTypeID", TypeCode.Int32, this.BindTypeInfoID.ToString());
        this.ObjectDataSourceCombo.DataBind();
        this.RepeaterCombo.DataSource = TableCombo;
        this.RepeaterCombo.DataBind();
    }

    /// <summary>
    /// Удалить список для Combo
    /// </summary>
    public void Del()
    {
        this.DeleteTableCombo();

        this.RepeaterCombo.DataSource = null;
        this.RepeaterCombo.DataBind();
    }

    /// <summary>
    /// сохранить список после ввода новых данных
    /// </summary>
    /// <param name="newInfoTypeID">ID нового типа</param>
    public void SaveNew(int newInfoTypeID)
    {
        this.SaveRepeaterToTableCombo();
        if (newInfoTypeID != 0)
            this.SaveTableComboToDatabase(newInfoTypeID);
    }

    /// <summary>
    /// Сохранить список после редактирования
    /// </summary>
    public void SaveEdit()
    {
        this.SaveRepeaterToTableCombo();
        this.DeleteErasedRowToDatabase();
        this.SaveTableComboToDatabase(this.BindTypeInfoID);
    }

    /// <summary>
    /// Показать список для комбо
    /// </summary>
    /// <param name="isNew">true - список дял нового типа</param>
    public void Show(bool isNew)
    {
        this.LabelHeaderCombo.Visible = true;
        this.LabelAddCombo.Visible = true;
        this.LabelAddComboNum.Visible = true;
        this.TextBoxAddCombo.Visible = true;
        this.TextBoxAddComboNum.Visible = true;
        this.ButtonAddCombo.Visible = true;

        //todo: если InfoTypeID тот же и таблица tblCombo существует то инициализировать не нада

        this.InitCombo(isNew);
        this.RepeaterCombo.DataSourceID = null;
        this.RepeaterCombo.DataSource = TableCombo;

        this.RepeaterCombo.DataBind();
    }

    /// <summary>
    /// Спрятать список для Combo
    /// </summary>
    public void Hide()
    {
        this.LabelHeaderCombo.Visible = false;
        this.LabelAddCombo.Visible = false;
        this.LabelAddComboNum.Visible = false;
        this.TextBoxAddCombo.Visible = false;
        this.TextBoxAddComboNum.Visible = false;
        this.ButtonAddCombo.Visible = false;

        this.RepeaterCombo.DataSource = null;
        this.RepeaterCombo.DataBind();
    }

    /// <summary>
    /// ДОБАВИТЬ строку в КомбоБоксе
    /// </summary>
    protected void ButtonAddCombo_Click(object sender, EventArgs e)
    {
        //сохраним отредактированые на экране данные
        this.SaveRepeaterToTableCombo();

        if (TableCombo != null)
        {
            DataRow row;
            row = TableCombo.NewRow();
            row["Name"] = this.TextBoxAddCombo.Text;
            int res;
            if (int.TryParse(this.TextBoxAddComboNum.Text, out res))
                row["NumSorted"] = res;
            else
                row["NumSorted"] = row["ID"];
            TableCombo.Rows.Add(row);

            this.RepeaterCombo.DataSource = TableCombo;
            this.RepeaterCombo.DataBind();

            this.TextBoxAddCombo.Text = string.Empty;
            this.TextBoxAddComboNum.Text = string.Empty;
        }
    }

    /// <summary>
    /// УДАЛИТЬ строку в таблице для комбобокса
    /// </summary>
    protected void ButtonDelCombo_Click(object sender, EventArgs e)
    {
        //todo: проверить связи при удалении
        // что будет если ID удаляемой уже где то используется - а там будет пусто
        if (TableCombo != null)
        {
            RepeaterItem it = (RepeaterItem)((Button)sender).Parent;
            TableCombo.Rows.RemoveAt(it.ItemIndex);
            this.RepeaterCombo.DataSource = TableCombo;
            this.RepeaterCombo.DataBind();
        }
    }

    /// <summary>
    /// СОЗДАЁМ таблицу для КомбоБокса
    /// </summary>
    /// <param name="isNew">true если новые данные</param>
    private void InitCombo(bool isNew)
    {
        if (TableCombo != null) this.DeleteTableCombo();

        TableCombo = new DataTable("tblCombo");
        TableCombo.Columns.Add(new DataColumn("ID", Type.GetType("System.Int32")));
        TableCombo.Columns.Add(new DataColumn("Name", Type.GetType("System.String")));
        TableCombo.Columns.Add(new DataColumn("NumSorted", Type.GetType("System.Int32")));
        TableCombo.Columns["ID"].AllowDBNull = false;
        TableCombo.Columns["ID"].DefaultValue = 0;
        TableCombo.Columns["Name"].AllowDBNull = true;
        TableCombo.Columns["Name"].DefaultValue = string.Empty;
        TableCombo.Columns["NumSorted"].AllowDBNull = false;
        TableCombo.Columns["NumSorted"].DefaultValue = "100";

        if (!isNew)
        {
            // загружаем данные из реальной бызы
            DataSet1TableAdapters.InfoTypeComboTableAdapter adapterCombo = new DataSet1TableAdapters.InfoTypeComboTableAdapter();
            DataSet1.InfoTypeComboDataTable dataTableCombo = adapterCombo.GetData(this.BindTypeInfoID);

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
    /// удалить таблицу для Combo
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
            foreach (RepeaterItem ri in this.RepeaterCombo.Items)
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
    /// сохранить временную таблицу tblCombo в Базу данных
    /// </summary>
    private void SaveTableComboToDatabase(int saveInfoTypeID)
    {
        if (TableCombo != null)
        {
            //можно сохранять только если тип combo, но сохраню ка я всегда, хуже не будет!

            DataSet1TableAdapters.InfoTypeComboTableAdapter adapterCombo = new DataSet1TableAdapters.InfoTypeComboTableAdapter();

            foreach (DataRow row in TableCombo.Rows)
            {
                adapterCombo.SaveRow((int)row["ID"], (string)row["Name"], saveInfoTypeID, (int)row["NumSorted"]);
            }
        }
    }

    /// <summary>
    /// удалить из реальной базы строки удалённые во временной tblCombo
    /// </summary>
    private void DeleteErasedRowToDatabase()
    {
        if (TableCombo != null)
        {
            // берём для сравнения данные из реальной бызы
            DataSet1TableAdapters.InfoTypeComboTableAdapter adapterCombo = new DataSet1TableAdapters.InfoTypeComboTableAdapter();
            DataSet1.InfoTypeComboDataTable dataTableCombo = adapterCombo.GetData(this.BindTypeInfoID);

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
