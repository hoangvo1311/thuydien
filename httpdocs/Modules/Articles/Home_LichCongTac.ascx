<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Home_LichCongTac.ascx.cs"
    Inherits="Modules_Articles_Home_LichCongTac" %>

<div id="p_calendar1">
    <div id="p_calendar2">
        <asp:Repeater ID="rptArticles" runat="server">
            <HeaderTemplate>
                <ul class="list-unstyled">
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <a href="<%# ViewDetailsURL + "?aid=" + DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.ArticleID)%>"><%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.Title)%><%--&nbsp;<img src="<%= MMC.VTT.Properties.WEB_CONFIG.ApplicationPath %>/images/new.gif" />--%></a>
                    <%--<asp:LinkButton ID="aViewDetails" runat="server" PostBackUrl='<%# ViewDetailsURL + "?aid=" + DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.ArticleID)%>'><%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.Title)%> </asp:LinkButton>--%>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul></FooterTemplate>
        </asp:Repeater>
        <%--<asp:Literal ID="lblLichCongTac" runat="server"></asp:Literal>--%>
    </div>
</div>