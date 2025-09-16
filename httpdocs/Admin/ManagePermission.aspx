<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true" CodeFile="ManagePermission.aspx.cs"
    Inherits="Admin_ManagePermission" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">Quản lý phân quyền</div>
        <div class="panel-body">
            <div id="content-area" class="text-center">                
                <div class="acp-module">
                    <a class="acp-hicon" href="AdminControlPanel.aspx"><img src="../Admin/images/icons_home.jpg" /></a>
                    <a class="acp-link" href="AdminControlPanel.aspx">Trang chủ Quản lý</a></div>
                <div class="acp-module" id="ManageCategories" runat="server" visible='<%# getPermission("Grouplist.aspx") %>'>                        
                    <a class="acp-hicon" href="Grouplist.aspx"><img src="../Admin/images/ico_user_group.jpg" /></a>                        
                    <a href="Grouplist.aspx" class="acp-link">Quản lý nhóm</a>
                </div>
                <div class="acp-module" id="ManageArticles" runat="server" visible='<%# getPermission("DM_Userlist.aspx") %>'>
                    <a class="acp-hicon" href="DM_Userlist.aspx"><img src="../Admin/images/ico_user_mem.jpg" /></a>
                    <a href="DM_Userlist.aspx" class="acp-link">Quản lý người dùng</a>
                </div>
                <div class="acp-module" id="Li1" runat="server" visible='<%# getPermission("DM_userRegister.aspx") %>'>
                    <a class="acp-hicon" href="DM_userRegister.aspx"><img src="../Admin/images/ico_user_acc.jpg" /></a>                        
                    <a href="DM_userRegister.aspx" class="acp-link">Tạo tài khoản</a>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
