<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true" CodeFile="ManageArticles.aspx.cs" Inherits="Admin_ManageArticles" Title="" ValidateRequest="false"%>

<%@ Register Src="Modules/Articles/ArticlesConfig.ascx" TagName="ArticlesConfig"
    TagPrefix="uc4" %>

<%@ Register Src="Modules/Articles/ArticleUpdate.ascx" TagName="ArticleUpdate" TagPrefix="uc3" %>

<%@ Register Src="Modules/Articles/ArticleAddnew.ascx" TagName="ArticleAddnew" TagPrefix="uc2" %>

<%@ Register Src="Modules/Articles/ArticlesList.ascx" TagName="ArticlesList" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading"><asp:Label id="lblTitle" runat="server"></asp:Label></div>
        <div class="panel-body">
            <ul id="toolbox" >
                <li>
                    <asp:ImageButton ID="imgHome" runat="server" ImageUrl="~/Admin/images/ad_icon_home.gif" OnClick="imgHome_Click" />
                    <br />
                    <asp:LinkButton ID="linkHome" runat="server" CssClass="toolbox-link" OnClick="linkHome_Click">Trang chủ</asp:LinkButton>
                </li>
                <li>
                    <asp:ImageButton ID="imgConfig" runat="server" ImageUrl="~/Admin/images/confg_24.gif" OnClick="imgConfig_Click" ValidationGroup="Article" />
                    <br />
                    <asp:LinkButton ID="linkConfig" runat="server" CssClass="toolbox-link" OnClick="linkConfig_Click" ValidationGroup="Article">Cấu hình</asp:LinkButton>
                </li>
                <li>
                    <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/Admin/images/ad_icon_save.gif" OnClick="imgSave_Click" ValidationGroup="Article" />
                    <br />
                    <asp:LinkButton ID="linkSave" runat="server" CssClass="toolbox-link" OnClick="linkSave_Click" ValidationGroup="Article">Lưu</asp:LinkButton>
                </li>
                <li id="icon-close">
                    <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/Admin/images/ad_icon_clo.gif" OnClick="imgClose_Click" />
                    <br />
                    <asp:LinkButton ID="linkClose" runat="server" CssClass="toolbox-link" OnClick="linkClose_Click" >Đóng lại</asp:LinkButton>
                </li>
                <li id="icon-add">
                    <asp:ImageButton ID="imgAdd" runat="server" ImageUrl="~/Admin/images/ad_icon_add.gif" OnClick="imgAdd_Click" />
                    <br />
                    <asp:LinkButton ID="linkAdd" runat="server" CssClass="toolbox-link" OnClick="linkAdd_Click" >Thêm mới</asp:LinkButton>
                </li>
                <li>
                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Admin/images/ad_icon_edit.gif" OnClick="imgEdit_Click" />
                    <br />
                    <asp:LinkButton ID="linkEdit" runat="server" CssClass="toolbox-link" OnClick="linkEdit_Click">Sửa đổi</asp:LinkButton></li><li id="icon-del">
                    <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Admin/images/ad_icon_del.gif" OnClick="imgDel_Click" />
                    <br />
                        <asp:LinkButton ID="linkDelete" runat="server" CssClass="toolbox-link" OnClick="linkDelete_Click">Xoá bỏ </asp:LinkButton></li></ul>
            <cc1:ConfirmButtonExtender ID="confirmDel" runat="server" TargetControlID="imgDel" ConfirmText="Bạn có muốn xóa?">
            </cc1:ConfirmButtonExtender>
            <cc1:ConfirmButtonExtender ID="confirmDel2" runat="server" TargetControlID="linkDelete" ConfirmText="Bạn có muốn xóa?">
            </cc1:ConfirmButtonExtender>

            <!-- #ul:toolbox -->

        <div id="module-tile-area">            
            <div id="dropdown-area">
                <label>Chọn chuyên mục:&nbsp;</label>                
                <asp:DropDownList ID="ddlCategories" runat="server" Width="300px" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </div>
        </div><!-- #div:module-tile-area -->
        <div id="content-area">            
            <uc1:ArticlesList ID="ArticlesList1" runat="server" OnDetailsClick="linkEdit_Click"/>
            <uc2:ArticleAddnew ID="ArticleAddnew1" runat="server" />
            <uc3:ArticleUpdate ID="ArticleUpdate1" runat="server" />
            <uc4:ArticlesConfig id="ArticlesConfig1" runat="server" />        
        </div>

        </div>
    </div>



</asp:Content>

