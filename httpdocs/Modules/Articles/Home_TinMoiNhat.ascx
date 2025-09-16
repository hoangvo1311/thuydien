<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Home_TinMoiNhat.ascx.cs"
    Inherits="Modules_Articles_Home_TinMoiNhat" %>
    <h4 class="module-bar">
                        TIN MỚI NHẤT<span></span></h4>
                        <div id="p-hot-news">

    <asp:Repeater ID="rptArticlesHot" runat="server">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <div class="short-desc2">
                <asp:LinkButton ID="aViewDetails" runat="server" PostBackUrl='<%# ViewDetailsURL + "?aid=" + DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.ArticleID)%>'><%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.Title)%><span class="new-blink"></span> </asp:LinkButton>
                 <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.AddedDate)).AddHours(24) > DateTime.Now ? "" : ""%>
                 <p><%# Server.HtmlDecode(DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Articles.ColumnNames.Abstract).ToString())%></p>
                 </div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul></FooterTemplate>
    </asp:Repeater>
</div>
<!-- enddiv: #h-latest -->

 
                    