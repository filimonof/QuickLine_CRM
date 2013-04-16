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
/// Класс- список с возможными типами дополнительной информации
/// </summary>
public class TypeInfo
{
    public const string TI_STRING = "string";
    public const string TI_INT = "int";
    public const string TI_BOOL = "bool";
    public const string TI_COMBO = "combo";
    public const string TI_TEL = "telefon";
    
    private ArrayList alType;

    public TypeInfo()
    {
        this.alType = new ArrayList();
        this.alType.Add(TI_STRING);
        this.alType.Add(TI_INT);
        this.alType.Add(TI_BOOL);
        this.alType.Add(TI_COMBO);
        this.alType.Add(TI_TEL);
    }

    /// <summary>
    /// для Darasource набор данных
    /// </summary>
    /// <returns>список с типами</returns>
    public ArrayList List()
    {
        return this.alType;
    }

    /// <summary>
    /// преобразовать object(строка) в Bool
    /// </summary>
    /// <param name="value">значение</param>
    /// <param name="default">значение по умолчанию если строка некорректная</param>
    /// <returns>true/false</returns>
    public static bool ToBool(object value, bool @default)
    {
        bool result;        
        if (!Boolean.TryParse(value.ToString(), out result))
            result = @default;
        return result;
    }

    /// <summary>
    /// преобразовать object(строка) в Bool
    /// </summary>
    /// <param name="val">значение</param>
    /// <returns>true/false</returns>
    public static bool ToBool(object val)
    {
        return ToBool(val, true);
    }

    /// <summary>
    /// этот тип - строка? val=TI_STRING
    /// </summary>
    /// <param name="val">значение(строковое)</param>
    /// <returns>да/нет</returns>
    public static bool IsString(object val)
    {
        return String.Equals((string)val, TI_STRING);
    }

    /// <summary>
    /// этот тип - число? val=TI_INT
    /// </summary>
    /// <param name="val">значение(строковое)</param>
    /// <returns>да/нет</returns>
    public static bool IsInt(object val)
    {
        return String.Equals((string)val, TI_INT);
    }

    /// <summary>
    /// этот тип - список? val=TI_COMBO
    /// </summary>
    /// <param name="val">значение(строковое)</param>
    /// <returns>да/нет</returns>
    public static bool IsCombo(object val)
    {
        return String.Equals((string)val, TI_COMBO);
    }

    /// <summary>
    /// этот тип - будевый? val=TI_BOOL
    /// </summary>
    /// <param name="val">значение(строковое)</param>
    /// <returns>да/нет</returns>
    public static bool IsBool(object val)
    {
        return String.Equals((string)val, TI_BOOL);
    }

    /// <summary>
    /// этот тип - телефон? val=TI_TEL
    /// </summary>
    /// <param name="val">значение(строковое)</param>
    /// <returns>да/нет</returns>
    public static bool IsTel(object val)
    {
        return String.Equals((string)val, TI_TEL);
    }
}
