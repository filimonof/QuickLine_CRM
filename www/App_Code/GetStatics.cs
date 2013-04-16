using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// ����� �� ��������������� ����������� ��� ������ ������ �� ���� 
/// ��� ��������������� ��������������� ���������
/// </summary>
public class GetStatics
{

    public GetStatics()
    {
    }

    /// <summary>
    /// �� ID ������ ��� ������
    /// </summary>
    /// <param name="id">ID ������</param>
    /// <returns>��� ������</returns>
    public static string GetNameAgent(object id)
    {
        if (id != null)
        {
            DataSet1TableAdapters.AgentsTableAdapter adapterAgents = new DataSet1TableAdapters.AgentsTableAdapter();
            object name = adapterAgents.GetNameAgent((int)id);
            if (name != null) return name.ToString();
            else return string.Empty;
        }
        else return string.Empty;
    }

    /// <summary>
    /// �� ID ������ ��� ������
    /// </summary>
    /// <param name="id">ID ������</param>
    /// <returns>��� ������</returns>
    public static string GetGroupAgent(object id)
    {
        if (id != null)
        {
            DataSet1TableAdapters.AgentsTableAdapter adapterAgents = new DataSet1TableAdapters.AgentsTableAdapter();
            object name = adapterAgents.GetGroupAgent((int)id);
            if (name != null) return name.ToString();
            else return string.Empty;
        }
        else return string.Empty;
    }

    /// <summary>
    /// �� ID ������ ��� �������, ����� ��� �����������
    /// </summary>
    /// <param name="id">ID �������</param>
    /// <returns>��� �������</returns>
    public static string GetNameClient(object id)
    {
        if (id != null)
        {
            DataSet1TableAdapters.ClientsTableAdapter adapterClients = new DataSet1TableAdapters.ClientsTableAdapter();
            object name = adapterClients.GetNameClient((int)id);
            if (name != null) return name.ToString();
            else return string.Empty;
        }
        else return string.Empty;
    }

    /// <summary>
    /// �� ID ������ ��� ��������, ����� ��� �����������
    /// </summary>
    /// <param name="id">ID ��������</param>
    /// <returns>��� ��������</returns>
    public static string GetNameContact(object id)
    {
        if (id != null)
        {
            DataSet1TableAdapters.ContactsTableAdapter adapterContacts = new DataSet1TableAdapters.ContactsTableAdapter();
            object name = adapterContacts.GetNameContact((int)id);
            if (name != null) return name.ToString();
            else return string.Empty;
        }
        else return string.Empty;
    }

    /// <summary>
    /// �� ID ������ ��� ������, ����� ��� �����������
    /// </summary>
    /// <param name="id">ID ������</param>
    /// <returns>��� ������</returns>
    public static string GetNameTask(object id)
    {
        if (id != null)
        {
            DataSet1TableAdapters.TasksTableAdapter adapterTasks = new DataSet1TableAdapters.TasksTableAdapter();
            object name = adapterTasks.GetNameTask((int)id);
            if (name != null) return name.ToString();
            else return string.Empty;
        }
        else return string.Empty;
    }

    /// <summary>
    /// �� ID ������ �������� ���������, ����� ��� �����������
    /// </summary>
    /// <param name="id">ID ���������</param>
    /// <returns>�������� ���������</returns>
    public static string GetNameCategory(object id)
    {
        if (id != null)
        {
            DataSet1TableAdapters.CategoryTableAdapter adapterCategory = new DataSet1TableAdapters.CategoryTableAdapter();
            object name = adapterCategory.GetNameCategory((int)id);
            if (name != null) return name.ToString();
            else return string.Empty;
        }
        else return string.Empty;
    }

    /// <summary>
    /// �� ID ������ ��� ����, ����� ��� �����������
    /// </summary>
    /// <param name="id">ID ����</param>
    /// <returns>��� ����</returns>
    public static string GetNameType(object id)
    {
        if (id != null)
        {
            DataSet1TableAdapters.ActivitieTypeTableAdapter adapterType = new DataSet1TableAdapters.ActivitieTypeTableAdapter();
            object name = adapterType.GetNameType((int)id);
            if (name != null) return name.ToString();
            else return string.Empty;
        }
        else return string.Empty;
    }

    /// <summary>
    /// ��������� - ������ ������� ���
    /// </summary>
    public const string SEX_MEN = " ������� ";
    /// <summary>
    /// ��������� - ������ ������� ���
    /// </summary>
    public const string SEX_WOMEN = " ������� ";

    /// <summary>
    /// �������������� ���������� ���������� � ��� (true->�������; false->�������)
    /// </summary>
    /// <param name="b">���������� bool</param>
    /// <returns>true->�������; false->�������</returns>
    public static string BoolToSex(object b)
    {
        bool res;
        if (b != null && Boolean.TryParse(b.ToString(), out res))
        {
            if (res) return SEX_MEN;
            else return SEX_WOMEN;
        }
        else return string.Empty;
    }

    /// <summary>
    /// ���������� �� ���������� clientID taskID contactID � ������ ������� ��� ���������
    /// </summary>
    /// <param name="clientID">������</param>
    /// <param name="taskID">�����</param>
    /// <param name="contactID">�������</param>
    /// <returns>ID �������</returns>
    public static int GetClientID(int clientID, int taskID, int contactID)
    {
        if (clientID > 0)
        {
            return clientID;
        }
        else if (taskID > 0)
        {
            DataSet1TableAdapters.TasksTableAdapter adapterTask = new DataSet1TableAdapters.TasksTableAdapter();
            object id1 = adapterTask.GetClientIDToTaskID(taskID);
            if (id1 != null) return (int)id1;
            else return 0;
        }
        else if (contactID > 0)
        {
            DataSet1TableAdapters.ContactsTableAdapter adapterContact = new DataSet1TableAdapters.ContactsTableAdapter();
            object id2 = adapterContact.GetClientIDToContactID(contactID);
            if (id2 != null) return (int)id2;
            else return 0;
        }
        else return 0;
    }
    
    /// <summary>
    /// ���������� �� ��������� activitieID � ������ ������� ��� ���������
    /// </summary>
    /// <param name="activitieID">��������</param>
    /// <returns>ID �������</returns>
    public static int GetClientIDToActivitie(int activitieID)
    {
        if (activitieID > 0)
        {
            DataSet1TableAdapters.ActivitiesTableAdapter adapterActivitie = new DataSet1TableAdapters.ActivitiesTableAdapter();
            object id = adapterActivitie.GetClientIDToActivitieID(activitieID);
            if (id != null) return (int)id;
            else return 0;
        }
        else return 0;    
    }

    /// <summary>
    /// ���������� �� ��������� activitieID � ������ �������� ���������
    /// </summary>
    /// <param name="activitieID">��������</param>
    /// <returns>ID ��������</returns>
    public static int GetContactIDToActivitie(int activitieID)
    {
        if (activitieID > 0)
        {
            DataSet1TableAdapters.ActivitiesTableAdapter adapterActivitie = new DataSet1TableAdapters.ActivitiesTableAdapter();
            object id = adapterActivitie.GetContactIDToActivitieID(activitieID);
            if (id != null) return (int)id;
            else return 0;
        }
        else return 0;
    }

    /// <summary>
    /// ���������� �� ��������� activitieID � ����� ������ ���������
    /// </summary>
    /// <param name="activitieID">��������</param>
    /// <returns>ID ������</returns>
    public static int GetTaskIDToActivitie(int activitieID)
    {
        if (activitieID > 0)
        {
            DataSet1TableAdapters.ActivitiesTableAdapter adapterActivitie = new DataSet1TableAdapters.ActivitiesTableAdapter();
            object id = adapterActivitie.GetTaskIDToActivitieID(activitieID);
            if (id != null) return (int)id;
            else return 0;
        }
        else return 0;
    }

    /// <summary>
    /// ������ ID ������� � ������ ������� (����������� ��� ���������, ���� �� ����������� �������)
    /// </summary>
    /// <returns>ID �������</returns>
    public static int GetFirstClientID()
    {

        DataSet1TableAdapters.ClientsTableAdapter adapterClient = new DataSet1TableAdapters.ClientsTableAdapter();
        DataSet1.ClientsDataTable dt = adapterClient.GetData();
        object val = dt[0]["ID"];
        if (val != null) return (int)val;
        else return 0;
    }

}

