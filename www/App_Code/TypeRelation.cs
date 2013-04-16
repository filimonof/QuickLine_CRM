using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// Класс - список, какой таблице принадлежат "дополнительные типы информации" 
/// </summary>
public class TypeRelation
{
    public const string TR_ALL = "all";
    public const string TR_CLIENT = "client";
    public const string TR_CONTACT = "contact";
    public const string TR_TASK = "task";
    public const string TR_ACTIVITIE = "activitie";

    private ArrayList alTypeRelation;

    public TypeRelation()
    {
        this.alTypeRelation = new ArrayList();
        this.alTypeRelation.Add(TR_ALL);
        this.alTypeRelation.Add(TR_CLIENT);
        this.alTypeRelation.Add(TR_CONTACT);
        this.alTypeRelation.Add(TR_TASK);
        this.alTypeRelation.Add(TR_ACTIVITIE);
    }

    /// <summary>
    /// для Darasource набор данных
    /// </summary>
    /// <returns>список с типами</returns>
    public ArrayList List()
    {
        return this.alTypeRelation;
    }

}