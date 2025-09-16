<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticlesHot.ascx.cs" Inherits="Modules_Articles_ArticlesHot" %>
<asp:Repeater ID="rptArticlesHot" runat="server">
    <HeaderTemplate>
        <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <asp:LinkButton ID="aViewDetails" runat="server" CssClass="lnews" PostBackUrl='<%# ViewDetailsURL + "?aid=" + DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.ArticleID)%>'><%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.Title)%></asp:LinkButton>
            <span class="news-date">&nbsp;(<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.AddedDate)).ToString("dd/MM/yyyy") %>)</span>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul></FooterTemplate>
</asp:Repeater>
