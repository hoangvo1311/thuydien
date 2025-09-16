<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Home_GioiThieu.ascx.cs" Inherits="Modules_Articles_Home_GioiThieu" %>

<div class="news-section clearfix" id="news-section-intro">
    <h4 class="mod-title"><a href="javascript:void(0)"><span class="red-bar">Giới thiệu chung</span></a></h4>
    <ul class="headline-section clearfix">
        <li style="width:100% !important; height:auto !important; overflow:initial;">
            <a href="News.aspx?aid=886"><asp:Literal id="lblTitle" runat="server" ></asp:Literal></a>
            <p>
                <asp:Literal ID="lblContent" runat="server" Text=""></asp:Literal>
                <asp:LinkButton CssClass="intro-more" ID="linkViewDetails" runat="server">Xem tiếp &raquo;</asp:LinkButton>
            </p>
        </li>
    </ul>

    
</div>