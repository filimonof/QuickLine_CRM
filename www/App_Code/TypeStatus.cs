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
/// Класс - список, возможные значения статуса задачи
/// </summary>
public class TypeStatus
{
    public const string TS_OPEN = "open";
    public const string TS_CLOSE = "close";

    private ArrayList alTypeStatus;

    public TypeStatus()
    {
        this.alTypeStatus = new ArrayList();
        this.alTypeStatus.Add(TS_OPEN);
        this.alTypeStatus.Add(TS_CLOSE);
    }

    /// <summary>
    /// для Darasource набор данных
    /// </summary>
    /// <returns>список со статусами</returns>
    public ArrayList List()
    {
        return this.alTypeStatus;
    }

    /// <summary>
    /// По порядковому номеру статуса определяем его строковый эквивалент
    /// </summary>
    /// <param name="status">целое число, порядковый номер статуса</param>
    /// <returns>строка с названием статуса</returns>
    public static string StatusToString(object status)
    {
        int st;
        if (status != null && int.TryParse(status.ToString(), out st))
        {
            switch (st)
            {
                case (0): return TS_OPEN;
                case (1): return TS_CLOSE;
                default: return string.Empty;
            }
        }
        else return string.Empty;
    }

}