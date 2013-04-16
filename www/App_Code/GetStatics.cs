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
/// Класс со статистическими процедурами для взятия данных из базы 
/// или дополнительными статистическими функциями
/// </summary>
public class GetStatics
{

    public GetStatics()
    {
    }

    /// <summary>
    /// по ID узнаем ИМЯ агента
    /// </summary>
    /// <param name="id">ID агента</param>
    /// <returns>имя агента</returns>
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
    /// по ID узнаем ТИП агента
    /// </summary>
    /// <param name="id">ID агента</param>
    /// <returns>тип агента</returns>
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
    /// по ID узнаем ИМЯ клиента, нужно для детализации
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <returns>имя клиента</returns>
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
    /// по ID узнаем ИМЯ контакта, нужно для детализации
    /// </summary>
    /// <param name="id">ID контакта</param>
    /// <returns>имя контакта</returns>
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
    /// по ID узнаем ИМЯ задачи, нужно для детализации
    /// </summary>
    /// <param name="id">ID задачи</param>
    /// <returns>имя задачи</returns>
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
    /// по ID узнаем название КАТЕГОРИИ, нужно для детализации
    /// </summary>
    /// <param name="id">ID категории</param>
    /// <returns>название категории</returns>
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
    /// по ID узнаем ИМЯ типа, нужно для детализации
    /// </summary>
    /// <param name="id">ID типа</param>
    /// <returns>имя типа</returns>
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
    /// Константа - строка мужской пол
    /// </summary>
    public const string SEX_MEN = " мужской ";
    /// <summary>
    /// Константа - строка женский пол
    /// </summary>
    public const string SEX_WOMEN = " женский ";

    /// <summary>
    /// Преобразование логической переменной в ПОЛ (true->мужской; false->женский)
    /// </summary>
    /// <param name="b">переменная bool</param>
    /// <returns>true->мужской; false->женский</returns>
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
    /// Определить по параметрам clientID taskID contactID к какому клиенту они относятся
    /// </summary>
    /// <param name="clientID">клиент</param>
    /// <param name="taskID">здача</param>
    /// <param name="contactID">контакт</param>
    /// <returns>ID клиента</returns>
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
    /// Определить по параметру activitieID к какому клиенту они относятся
    /// </summary>
    /// <param name="activitieID">действие</param>
    /// <returns>ID клиента</returns>
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
    /// Определить по параметру activitieID к какому контакту относится
    /// </summary>
    /// <param name="activitieID">действие</param>
    /// <returns>ID контакта</returns>
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
    /// Определить по параметру activitieID к какой задаче относится
    /// </summary>
    /// <param name="activitieID">действие</param>
    /// <returns>ID задачи</returns>
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
    /// узнает ID первого в списке клиента (понадобился для дропдауна, если не понадобится удалить)
    /// </summary>
    /// <returns>ID клиента</returns>
    public static int GetFirstClientID()
    {

        DataSet1TableAdapters.ClientsTableAdapter adapterClient = new DataSet1TableAdapters.ClientsTableAdapter();
        DataSet1.ClientsDataTable dt = adapterClient.GetData();
        object val = dt[0]["ID"];
        if (val != null) return (int)val;
        else return 0;
    }

}

