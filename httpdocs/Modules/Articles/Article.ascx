<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Article.ascx.cs" Inherits="Modules_Articles_Article" %>

<h2 class="article-title"><asp:Literal ID="lblTitle" runat="server" Text=""></asp:Literal></h2>
<p><asp:Literal ID="lblContent" runat="server" Text=""></asp:Literal></p>

<div id="rf-detail-toolbox"></div>

<div id="other-articles">
    <asp:Repeater ID="rptRelArticles" runat="server">
        <HeaderTemplate>
            <div class="title"><h4>Xem tiếp bài viết khác:</h4></div>
            <ul class="section-recent list-unstyled clearfix">
        </HeaderTemplate>
        <ItemTemplate>
            <li  class="recent-news-txt"><a href='News.aspx?aid=<%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.ArticleID)%>&CategoryID=<%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.CategoryID)%>'>
                <%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.Title)%>
            </a></li>
        </ItemTemplate>
        <FooterTemplate>
            </ul></FooterTemplate>
    </asp:Repeater>
</div>

