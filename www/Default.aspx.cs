/*
TODO:
 
1
������� �������� "������":
DzinDzin.aspx?mode=outgoing_call&abon_number=89169906227     - ���������
DzinDzin.aspx?mode=incoming_call&abon_number=89169906227     - ��������  
 
2
ActiviteDetails ������ �� ��������� �������, ��� ����� ��� ������� ����� � 
 �������� (�� ����� �������� ��������� �����), ����� ������� ���� �� ��� � �������

3
����������� � ���������
  ������ �������� ����� ���� Deleted = bit, ������� ��� �������������� ������ ��������� 
  ����� ����������� ������ ��� ��� ��� ������ ��������, ����� ��� ������ �� �� ������ �������,
  :) ��� ��������� �������������� ���� ������� �� ���������� �������� �������
 
4
������� �����
 ������ ����� � �������
 
5 
� �������� InfoType ����������� � ��������������� ViewState, � �� ������ ���� ���� ����
 ��������� �� ��������, �� ���� ������� �� ����� ��� ����� ������������ ����� i
 

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