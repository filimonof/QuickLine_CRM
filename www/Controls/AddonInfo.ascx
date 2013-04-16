<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddonInfo.ascx.cs" Inherits="Controls_AddonInfo" %>
<%@ Register Src="AddonInfoEdit.ascx" TagName="AddonInfoEdit" TagPrefix="quli" %>
<%@ Register Src="AddonInfoView.ascx" TagName="AddonInfoView" TagPrefix="quli" %>
<%-- 
дополнительные данные, вкладки
--%>
<asp:Label ID="LabelAddonInfo" runat="server" Text="Дополнительные данные" Font-Bold="true"
    ForeColor="#ccaa66" />
<asp:MultiView ID="MultiViewAddon" runat="server" ActiveViewIndex="0">
    <asp:View ID="ViewAddonInfo" runat="server">
        <quli:AddonInfoView ID="AddonInfoView1" runat="server" />
    </asp:View>
    <asp:View ID="ViewAddonInfoWdit" runat="server">
        <quli:AddonInfoEdit ID="AddonInfoEdit1" runat="server" />
    </asp:View>
</asp:MultiView>