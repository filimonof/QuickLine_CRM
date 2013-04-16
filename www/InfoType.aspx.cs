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
    /// ������ ��� ��������� ID ���� ��� ���������� ������
    /// </summary>
    private int NewInfoTypeID = 0;

    /// <summary>
    /// ����������� ���������� ��� �������� ������� � �����������
    /// </summary>
    private static DataTable TableCombo;


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// ������ ������� ����� ���
    /// </summary>
    protected void ButtonAddType_Click(object sender, EventArgs e)
    {
        DetailsViewTypeInfo.ChangeMode(DetailsViewMode.Insert);
        MultiViewInfoType.ActiveViewIndex = 1;
    }

    /// <summary>
    /// ������ ��������� ���������
    /// </summary>
    protected void ButtonPost_Click(object sender, EventArgs e)
    {
        // ��������� / �������� ����        
        if (DetailsViewTypeInfo.CurrentMode == DetailsViewMode.Insert)
        {
            DetailsViewTypeInfo.InsertItem(true);
            // ��� ����� ������� ����� this.NewInfoTypeID

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
    /// ������� �� ����������� ���� - ����� ID ������ ����
    /// </summary>
    protected void ObjectDataSourceInfoTypeToID_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.NewInfoTypeID = Convert.ToInt32(e.OutputParameters["NewID"].ToString());
    }

    /// <summary>
    /// ������ �������� ���������
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
    /// ������ �������
    /// </summary>
    protected void ButtonDel_Click(object sender, EventArgs e)
    {
        //todo: ����� �������� ����� �� InfoTypeID (Combo) � �������� �������� �� ������

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
    /// ������ �������� ���������� - ��������� ���� �������������� ����������
    /// </summary>
    protected void GridViewList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //���� � ����� �������������� � ������������� �� �� 
        DetailsViewTypeInfo.ChangeMode(DetailsViewMode.Edit);

        MultiViewInfoType.ActiveViewIndex = 1;
    }

    /// <summary>
    /// ������� ������� ��� ����������
    /// </summary>
    /// <param name="IsNew">true ���� ����� ������</param>
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
            // ��������� ������ �� �������� ����
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
    /// ������� �� ��������� ���� ������ ��� ��������������
    /// </summary>
    protected void DropDownListTypeInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //todo: ����� ������� ����� ��� ��������� �������� �������� ������ ���� �����������, ������ ��?
        if (((DropDownList)sender).SelectedValue == "combo")
        {
            LabelHeaderCombo.Visible = true;
            LabelAddCombo.Visible = true;
            LabelAddComboNum.Visible = true;
            TextBoxAddCombo.Visible = true;
            TextBoxAddComboNum.Visible = true;
            ButtonAddCombo.Visible = true;

            //todo: ���� InfoTypeID ��� �� � ������� tblCombo ���������� �� ���������������� �� ����

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
    /// ������� �� �������� ������ � ���������� � ����� ������
    /// </summary>
    protected void DropDownListTypeInfo_DataBound(object sender, EventArgs e)
    {
        DropDownListTypeInfo_SelectedIndexChanged(sender, e);
    }

    /// <summary>
    /// ������� ������ � ������� ��� ����������
    /// </summary>
    protected void ButtonDelCombo_Click(object sender, EventArgs e)
    {
        //todo: ��������� ����� ��� ��������
        // ��� ����� ���� ID ��������� ��� ��� �� ������������ - � ��� ����� �����
        if (TableCombo != null)
        {
            RepeaterItem it = (RepeaterItem)((Button)sender).Parent;
            TableCombo.Rows.RemoveAt(it.ItemIndex);
            RepeaterCombo.DataSource = TableCombo;
            RepeaterCombo.DataBind();
        }
    }

    /// <summary>
    /// �������� ������ � ����������
    /// </summary>
    protected void ButtonAddCombo_Click(object sender, EventArgs e)
    {
        //�������� ���������������� �� ������ ������
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
    /// ������� ������� ��� Combo
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
    /// ��������� ������ � ������ �� ��������� ������� tblCombo
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
    /// ��������� ��������� ������� tblCombo � ���� ������
    /// </summary>
    private void SaveTableComboToDatabase(int saveInfoTypeID)
    {
        if (TableCombo != null)
        {
            //����� ��������� ������ ���� ��� combo, �� ������� �� � ������, ���� �� �����!

            DataSet1TableAdapters.InfoTypeComboTableAdapter adapterCombo = new DataSet1TableAdapters.InfoTypeComboTableAdapter();

            foreach (DataRow row in TableCombo.Rows)
            {
                adapterCombo.SaveRow((int)row["ID"], (string)row["Name"], saveInfoTypeID, (int)row["NumSorted"]);
            }
        }
    }

    /// <summary>
    /// ������� �� �������� ���� ������ �������� �� ��������� tblCombo
    /// </summary>
    private void DeleteErasedRowToDatabase()
    {
        if (TableCombo != null)
        {
            // ���� ��� ��������� ������ �� �������� ����
            DataSet1TableAdapters.InfoTypeComboTableAdapter adapterCombo = new DataSet1TableAdapters.InfoTypeComboTableAdapter();
            DataSet1.InfoTypeComboDataTable dataTableCombo = adapterCombo.GetData((int)DetailsViewTypeInfo.SelectedValue);

            if (dataTableCombo.Rows.Count > 0)
            {
                foreach (DataSet1.InfoTypeComboRow rowCombo in dataTableCombo.Rows)
                {
                    //���� ������ � tblCombo ���� �� �������, ���� ����� �� ������ �� ������
                    DataRow[] dRow = TableCombo.Select(" ID = " + rowCombo["ID"].ToString());
                    if (dRow.Length == 0)
                        adapterCombo.Delete((int)rowCombo["ID"]);
                }
            }

        }
    }

}
