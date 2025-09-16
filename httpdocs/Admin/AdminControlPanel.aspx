<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true"
    CodeFile="AdminControlPanel.aspx.cs" Inherits="Admin_AdminControlPanel" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<div class="panel panel-primary">
    <div class="panel-heading">Trang chủ quản lý</div>
    <div class="panel-body">
        <div id="content-area" class="text-center">
            <div class="acp-module" id="ManageCategories" runat="server">
                <a href="ManageCategories.aspx" class="acp-hicon"><img src="../Admin/images/acp_icon_categories.gif" /></a>
                <a href="ManageCategories.aspx" class="acp-link">Quản lý chuyên mục</a>
            </div><!-- enddiv: acp-module -->
            <div class="acp-module" id="ManageArticles" runat="server" visible='<%# getPermission("ManageArticles.aspx") %>'>
                <a href="ManageArticles.aspx" class="acp-hicon"><img src="../Admin/images/acp_icon_articles.gif" /></a>
                <a href="ManageArticles.aspx" class="acp-link">Quản lý tin bài</a>
            </div><!-- enddiv: acp-module -->
            <div class="acp-module" id="ManagePolls" runat="server" visible='<%# getPermission("ManagePolls.aspx") %>'>
                <a href="ManagePolls.aspx" class="acp-hicon"><img src="../Admin/images/acp_icon_report.gif" /></a>
                <a href="ManagePolls.aspx" class="acp-link">Thăm dò ý kiến</a>
            </div><!-- enddiv: acp-module -->
            <div class="acp-module" id="ManageGallery" runat="server" visible='<%# getPermission("ManageGallery.aspx") %>'>
                <a href="ManageGallery.aspx" class="acp-hicon"><img src="images/icon_album.gif" /></a>
                <a href="ManageGallery.aspx" class="acp-link">Quản lý Album</a>
            </div><!-- enddiv: acp-module -->
            <div class="acp-module" id="ManageAds" runat="server" visible='<%# getPermission("ManageAds.aspx") %>'>
                <a href="ManageAds.aspx" class="acp-hicon"><img src="../Admin/images/icon_man_ads.gif" /></a>
                <a href="ManageAds.aspx" class="acp-link">Quản lý quảng cáo</a>
            </div><!-- enddiv: acp-module -->
            <div class="acp-module" id="ManageVideo" runat="server" visible='<%# getPermission("ManageVideo.aspx") %>'>
                <a href="ManageVideo.aspx" class="acp-hicon"><img src="../Admin/images/icon_man_video.gif" /></a>
                <a href="ManageVideo.aspx" class="acp-link">Quản lý Video</a>
            </div><!-- enddiv: acp-module -->
            <div class="acp-module" id="ManageLinks" runat="server" visible='<%# getPermission("ManageLinks.aspx") %>'>
                <a href="ManageLinks.aspx" class="acp-hicon"><img src="../Admin/images/acp_icon_link.jpg" /></a>
                <a href="ManageLinks.aspx" class="acp-link">Quản lý liên kết</a>
            </div><!-- enddiv: acp-module -->
            <div class="acp-module" id="ManageFAQs" runat="server" visible='<%# getPermission("ManageFAQs.aspx") %>'>
                <a href="ManageFAQs.aspx" class="acp-hicon"><img src="../Admin/images/icon_man_faq.gif" /></a>
                <a href="ManageFAQs.aspx" class="acp-link">Quản lý FAQ</a>
            </div><!-- enddiv: acp-module -->
            <div class="acp-module" id="ManageOldMembers" runat="server" visible='<%# getPermission("ManageOldMembers.aspx") %>'>
                <a href="ManageOldMembers.aspx" class="acp-hicon"><img src="../Admin/images/icons_list_teacher.jpg" /></a>
                <a href="ManageOldMembers.aspx" class="acp-link">Danh sách cựu GV - HS</a>
            </div><!-- enddiv: acp-module -->
            <div class="acp-module" id="ManageSuggest" runat="server" visible='<%# getPermission("ManageSuggest.aspx") %>'>
                <a href="ManageSuggest.aspx" class="acp-hicon"><img src="../Admin/images/icons_gopy.jpg" /></a>
                <a href="ManageSuggest.aspx" class="acp-link">Góp ý</a>
            </div><!-- enddiv: acp-module -->
            <div class="acp-module" id="ManageContactUs" runat="server" visible='<%# getPermission("ManageContactUs.aspx") %>'>
                <a href="ManageContactUs.aspx" class="acp-hicon"><img src="../Admin/images/icon_contact.jpg" /></a>
                <a href="ManageContactUs.aspx" class="acp-link">Liên hệ</a>
            </div><!-- enddiv: acp-module -->
            <div class="acp-module" id="ManagePermission" runat="server" visible='<%# getPermission("ManagePermission.aspx") %>'>
                <a href="ManagePermission.aspx" class="acp-hicon"><img src="../Admin/images/icon_man_user.gif" /></a>
                <a href="ManagePermission.aspx" class="acp-link">Quản lý người dùng</a>
            </div><!-- enddiv: acp-module -->
        </div>
    </div>
</div>

</asp:Content>
