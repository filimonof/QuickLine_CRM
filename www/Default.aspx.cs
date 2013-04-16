/*
TODO:
 
1
Сделать страницу "ЗВОНОК":
DzinDzin.aspx?mode=outgoing_call&abon_number=89169906227     - исходящий
DzinDzin.aspx?mode=incoming_call&abon_number=89169906227     - входящий  
 
2
ActiviteDetails фильтр по дропдпуну Клиента, без этого при простом входе в 
 действия (не через клиентов контактов задач), можно сделать чёрт те что с данными

3
разобраться с удалением
  вместо удаления стоит поле Deleted = bit, поэтому при редактировании данных дропдауны 
  могут выбрасывать ошибку что мол нет такого значения, нужно без ошибок всё по людски сделать,
  :) или запретить редактирование если какойто из параметров числится удалёным
 
4
сделать поиск
 просто сесть и сделать
 
5 
В контроле InfoType разобраться с восстановлением ViewState, А ТО ЗАИПАЛ МЕНЯ ЭТОТ ПИДР
 впринципе всё работает, но душу терзает не менее чем образ комплексного числа i
 

*/
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Calendar1.SelectedDate = DateTime.Now.Date;
    }
}