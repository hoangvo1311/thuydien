<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FAQsList.ascx.cs" Inherits="Modules_FAQs_FAQsList" %>
<div class="box">
    <div class="box-top">
        <asp:LinkButton ID="aCategoryTitle" runat="server">FAQs - Những câu hỏi và trả lời thường gặp</asp:LinkButton>
    </div>
    <div class="box-content">
        <asp:Repeater ID="rptFAQs" runat="server">
            <HeaderTemplate>
                <ul class="faq-ul">
            </HeaderTemplate>
            <ItemTemplate>
                <li class="faq-question"><a href="#" class="news-title">
                    <%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_FAQs.ColumnNames.FAQ)%>
                </a></li>
                <li>
                    <%# Server.HtmlDecode(DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_FAQs.ColumnNames.Answer).ToString())%>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="box-bottom">
    </div>
</div>
