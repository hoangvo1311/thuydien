<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticleLatest.ascx.cs"
    Inherits="Modules_Articles_ArticleLatest" %>
<div class="box">
    <div class="box-top">
        <asp:LinkButton ID="aCategoryTitle" runat="server"></asp:LinkButton>
    </div>
    <div class="box-content">
        <div class="news-title">
            <asp:LinkButton ID="aArticleTitle" runat="server"></asp:LinkButton>
        </div>
        <div class="news-abstract">
            <asp:Label ID="lblArticleAbstract" runat="server"></asp:Label>
        </div>
        <div class="news-relation">
            <asp:Repeater ID="rptArticleRelation" runat="server">
                <HeaderTemplate>
                    <ul class="news-relation-items">
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:LinkButton ID="aViewDetails" runat="server" PostBackUrl='<%# ViewDetailsURL + "?aid=" + DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.ArticleID)%>'><%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.Title)%></asp:LinkButton>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="box-bottom">
    </div>
</div>
