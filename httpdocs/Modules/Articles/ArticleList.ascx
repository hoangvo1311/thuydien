<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticleList.ascx.cs" Inherits="Modules_Articles_ArticleList" %>
<div class="news-section mod-spacing clearfix">
    <asp:Repeater ID="rptArticles" runat="server">
        <HeaderTemplate>
            <ul class="article-list list-unstyled clearfix">
        </HeaderTemplate>
        <ItemTemplate>
            <li class="article-item clearfix">
            <a class="article-title" href='News.aspx?aid=<%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.ArticleID)%>&CategoryID=<%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.CategoryID)%>'>
                <%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.Title)%>
            </a><small class="datetime">(<%# DateTime.Parse(DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.AddedDate).ToString()).ToString("dd/MM")%>)</small><br />
                <div class="article-desc">
                    <%# Server.HtmlDecode(DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.Abstract).ToString())%>
                </div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</div>
<!-- Paging -->
<asp:Repeater ID="rptPager" runat="server">
    <HeaderTemplate>
        <nav class="clearfix">
            <ul class="pagination pagination-sm">
    </HeaderTemplate>
    <ItemTemplate>
        <li><a href='<%# DataBinder.Eval(Container.DataItem, "link")%>' class='<%# DataBinder.Eval(Container.DataItem, "css")%>'>
            <%# DataBinder.Eval(Container.DataItem, "page")%>
        </a></li>
    </ItemTemplate>
    <FooterTemplate>
            </ul>
        </nav>
    </FooterTemplate>
</asp:Repeater>
<!-- /Paging -->