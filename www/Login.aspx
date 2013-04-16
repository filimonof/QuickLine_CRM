<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PlayHosted - CRM - Авторизация</title>
    <style type="text/css">
    body
    {
	    background-color: white;
	    font-family: Sans-Serif, Verdana;
	    font-size: 12px;
	    color: #594f31;
    }
    .InputWord
    {
        font-family: Verdana, sans-serif, Tahoma, Helvetica; 
        font-size: 10px;	
        color: #594f31; 
        border: 1px solid #594f31; 
    }
    .TableWordSmall
    {
 	    border: 2px dashed #594f31; 
 	    padding: 0px 5px 0px 5px
    }
    a
    {
	    color: #594f31;
	    font-weight: normal;
	    text-decoration: none;
    }
    a:hover
    {
	    color: #997711;
	    text-decoration: underline;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <table align="center" valign="middle" class="TableWordSmall" border="0" cellpadding="0"
                cellspacing="0">
                <tr>
                    <td align="right" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="PlayHosted CRM авторизация"></asp:Label></td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100px" align="right">
                        <asp:Label ID="Label1" runat="server" Text="Логин"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxLogin" CssClass="InputWord" runat="server" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" style="width: 100px">
                        <asp:Label ID="Label2" runat="server" Text="Пароль"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextBoxPassword" CssClass="InputWord" runat="server" TextMode="Password"
                            Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Label ID="LabelError" runat="server" ForeColor="darkred" Font-Bold="true"></asp:Label></td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:LinkButton ID="ButtonLogon" runat="server" Text="  Вход  " Font-Bold="true"
                            OnClick="ButtonLogon_Click" /></td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
