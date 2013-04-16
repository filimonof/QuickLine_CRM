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

public partial class DzinDzin : System.Web.UI.Page
{
    /*
   Сделать страницу "ЗВОНОК":
   DzinDzin.aspx?mode=outgoing_call&abon_number=89169906227     - исходящий
   DzinDzin.aspx?mode=incoming_call&abon_number=89169906227     - входящий  
   */

    private static DataSet1.Dzin_FindTelefonDataTable tableDzin = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.Dzin();
    }

    private void Dzin()
    {
        # region Определение телефона

        object tel = Request.QueryString["abon_number"];
        if (tel != null)
        {
            this.LabelTel.Text = tel.ToString();

            DataSet1TableAdapters.Dzin_FindTelefonTableAdapter adapterDzin = new DataSet1TableAdapters.Dzin_FindTelefonTableAdapter();
            int? Count = 0;
            tableDzin = adapterDzin.GetData(tel.ToString(), ref Count);
            if (Count != null)
            {
                switch (Count)
                {
                    case (-1):
                        this.LiteralTelMsg.Text = "Нет ни одного активного типа для хранения телефонов";
                        this.LiteralTelMsg.Visible = true;
                        this.DropDownListTel.Visible = false;
                        break;
                    case (0):
                        this.LiteralTelMsg.Text = "Телефон не найден";
                        this.LiteralTelMsg.Visible = true;
                        this.DropDownListTel.Visible = false;
                        break;
                    case (1):
                        this.LiteralTelMsg.Text = tableDzin[0]["Name"].ToString();
                        this.LiteralTelMsg.Visible = true;
                        this.DropDownListTel.Visible = false;
                        this.SelectDzinDzin(tableDzin[0]["Relation"].ToString(), (int)tableDzin[0]["RelationID"]);
                        break;
                    default:
                        //this.LiteralTelMsg.Text = "  Телефон присутствует в " + Count.ToString() + " зависимостях    ";
                        this.LiteralTelMsg.Visible = false;
                        this.DropDownListTel.DataSource = tableDzin;
                        this.DropDownListTel.Visible = true;
                        this.DropDownListTel.DataBind();
                        this.SelectDzinDzin(tableDzin[0]["Relation"].ToString(), (int)tableDzin[0]["RelationID"]);
                        break;
                }
            }
        }

        #endregion

        #region Определение типа активности

        object mode = Request.QueryString["mode"];
        if (mode != null)
        {
            DataSet1TableAdapters.ActivitieTypeTableAdapter adapterActivitieType = new DataSet1TableAdapters.ActivitieTypeTableAdapter();
            object idActivitieType = adapterActivitieType.GetIDToName(mode.ToString());
            if (idActivitieType != null)
                this.DropDownListMode.SelectedValue = idActivitieType.ToString();
        }

        #endregion

        this.HiddenFieldStartDateTime.Value = DateTime.Now.ToString();

        this.AddonInfoDz.RelationTable = TypeRelation.TR_ACTIVITIE;
        this.AddonInfoDz.DataBind();
        this.AddonInfoDz.New();

    }

    /// <summary>
    /// Сохранить действие - звонок
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonSaveDzin_Click(object sender, EventArgs e)
    {
        //сохранить на автомате
        //CreationDateTime
        //CreationAgent
        //LastModificationDateTime
        //LastModificationAgentID
        //StartDateTime
        //StopDateTime
        //AgentID  
        //this.AddonInfoDz.Post(new ID);
        if (this.DropDownListContact.Visible && this.DropDownListContact.Items.Count > 0)
            this.HiddenFieldContact.Value = this.DropDownListContact.SelectedValue;
        if (this.DropDownListTask.Visible && this.DropDownListTask.Items.Count > 0)
            this.HiddenFieldTask.Value = this.DropDownListTask.SelectedValue;
        if (this.HiddenFieldTask.Value != "0" && this.HiddenFieldContact.Value != "0")
        {
            int? newID = 0;            
            DateTime? nextAction = null;
            DataSet1TableAdapters.ActivitiesTableAdapter adapterActivitie = new DataSet1TableAdapters.ActivitiesTableAdapter();
            adapterActivitie.Insert(
                int.Parse(this.HiddenFieldTask.Value),
                int.Parse(this.HiddenFieldContact.Value),
                (int)Session["CURRENT_AGENT"],
                int.Parse(this.DropDownListMode.SelectedValue),
                this.TextBoxName.Text,
                this.TextBoxNumber.Text,
                DateTime.Parse(this.HiddenFieldStartDateTime.Value),
                (int)Session["CURRENT_AGENT"],
                DateTime.Parse(this.HiddenFieldStartDateTime.Value),
                (int)Session["CURRENT_AGENT"],
                DateTime.Parse(this.HiddenFieldStartDateTime.Value),
                DateTime.Now,
                this.TextBoxResultAction.Text,
                this.TextBoxDateTimeNextAction.Text.Trim() == string.Empty ? nextAction : DateTime.Parse(this.TextBoxDateTimeNextAction.Text.Trim()),
                out newID);
            if (newID != null)
                this.AddonInfoDz.Post((int)newID);
        }
    }

    /// <summary>
    /// определён и выбран номер звонившего
    /// </summary>
    /// <param name="relation">тип базы</param>
    /// <param name="id">id</param>
    public void SelectDzinDzin(string relation, int id)
    {
        int clientID;
        switch (relation)
        {
            case (TypeRelation.TR_CLIENT):
                //client
                this.LabelClient.Text = GetStatics.GetNameClient(id);
                this.LabelClient.Visible = true;
                this.DropDownListClient.Visible = false;
                //contact
                this.ObjectDataSourceDropDownContacts.SelectParameters.Clear();
                this.ObjectDataSourceDropDownContacts.SelectParameters.Add("ClientID", TypeCode.Int32, id.ToString());
                this.ObjectDataSourceDropDownContacts.DataBind();
                this.LabelContact.Visible = false;
                this.DropDownListContact.Visible = true;
                this.HiddenFieldContact.Value = "0";
                //task
                this.ObjectDataSourceDropDownTasks.SelectParameters.Clear();
                this.ObjectDataSourceDropDownTasks.SelectParameters.Add("ClientID", TypeCode.Int32, id.ToString());
                this.ObjectDataSourceDropDownTasks.DataBind();
                this.LabelTask.Visible = false;
                this.DropDownListTask.Visible = true;
                this.HiddenFieldTask.Value = "0";
                break;
            case (TypeRelation.TR_CONTACT):
                //client
                clientID = GetStatics.GetClientID(0, 0, id);
                this.LabelClient.Text = GetStatics.GetNameClient(clientID);
                this.LabelClient.Visible = true;
                this.DropDownListClient.Visible = false;
                //contact
                this.LabelContact.Text = GetStatics.GetNameContact(id);
                this.LabelContact.Visible = true;
                this.DropDownListContact.Visible = false;
                this.HiddenFieldContact.Value = id.ToString();
                //task
                this.ObjectDataSourceDropDownTasks.SelectParameters.Clear();
                this.ObjectDataSourceDropDownTasks.SelectParameters.Add("ClientID", TypeCode.Int32, clientID.ToString());
                this.ObjectDataSourceDropDownTasks.DataBind();
                this.LabelTask.Visible = false;
                this.DropDownListTask.Visible = true;
                this.HiddenFieldTask.Value = "0";
                break;
            case (TypeRelation.TR_TASK):
                //client
                clientID = GetStatics.GetClientID(0, id, 0);
                this.LabelClient.Text = GetStatics.GetNameClient(clientID);
                this.LabelClient.Visible = true;
                this.DropDownListClient.Visible = false;
                //contact
                this.ObjectDataSourceDropDownContacts.SelectParameters.Clear();
                this.ObjectDataSourceDropDownContacts.SelectParameters.Add("ClientID", TypeCode.Int32, clientID.ToString());
                this.ObjectDataSourceDropDownContacts.DataBind();
                this.LabelContact.Visible = false;
                this.DropDownListContact.Visible = true;
                this.HiddenFieldContact.Value = "0";
                //task
                this.LabelTask.Text = GetStatics.GetNameTask(id);
                this.LabelTask.Visible = true;
                this.DropDownListTask.Visible = false;
                this.HiddenFieldTask.Value = id.ToString();
                break;
            case (TypeRelation.TR_ACTIVITIE):
                //client
                clientID = GetStatics.GetClientIDToActivitie(id);
                this.LabelClient.Text = GetStatics.GetNameClient(clientID);
                this.LabelClient.Visible = true;
                this.DropDownListClient.Visible = false;
                //contact
                int contactID = GetStatics.GetContactIDToActivitie(id);
                this.LabelContact.Text = GetStatics.GetNameContact(contactID);
                this.LabelContact.Visible = true;
                this.DropDownListContact.Visible = false;
                this.HiddenFieldContact.Value = contactID.ToString();
                //task
                int taskID = GetStatics.GetTaskIDToActivitie(id);
                this.LabelTask.Text = GetStatics.GetNameTask(taskID);
                this.LabelTask.Visible = true;
                this.DropDownListTask.Visible = false;
                this.HiddenFieldTask.Value = taskID.ToString();
                break;
        }
    }

    /// <summary>
    /// сделан выбор в дропдауне возможных номеров 
    /// </summary>
    protected void DropDownListTel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tableDzin != null)
        {
            int select = ((DropDownList)sender).SelectedIndex;
            this.SelectDzinDzin(tableDzin[select]["Relation"].ToString(), (int)tableDzin[select]["RelationID"]);
        }
    }

    /// <summary>
    /// прорисовка кнопки сохранения данных
    /// </summary>
    protected void ButtonSaveDzin_PreRender(object sender, EventArgs e)
    {
        ((Button)sender).Enabled =
            (this.HiddenFieldTask.Value != "0" || (this.DropDownListTask.Visible && this.DropDownListTask.Items.Count > 0))
            &&
            (this.HiddenFieldContact.Value != "0" || (this.DropDownListContact.Visible && this.DropDownListContact.Items.Count > 0));
    }
}
