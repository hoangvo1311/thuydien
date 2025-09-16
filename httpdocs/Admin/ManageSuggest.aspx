<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true"
    CodeFile="ManageSuggest.aspx.cs" Inherits="Admin_ManageSuggest" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="Modules/Suggest/MesssageList.ascx" TagName="MesssageList" TagPrefix="uc1" %>
<%@ Register Src="Modules/Suggest/MessageView.ascx" TagName="MessageView" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="module-top">
        <ul id="toolbox">
            <li>
                <asp:ImageButton ID="imgHome" runat="server" ImageUrl="~/Admin/images/ad_icon_home.gif"
                    OnClick="imgHome_Click" />
                <br />
                <asp:LinkButton ID="linkHome" runat="server" CssClass="toolbox-link" OnClick="linkHome_Click">Trang chủ Quản lý</asp:LinkButton>
            </li>
            <li id="icon-close">
                <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/Admin/images/ad_icon_clo.gif"
                    OnClick="imgClose_Click" />
                <br />
                <asp:LinkButton ID="linkClose" runat="server" CssClass="toolbox-link" OnClick="linkClose_Click">Đóng lại</asp:LinkButton>
            </li>
            <li id="icon-del">
                <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Admin/images/ad_icon_del.gif"
                    OnClick="imgDel_Click" />
                <br />
                <asp:LinkButton ID="linkDelete" runat="server" CssClass="toolbox-link" OnClick="linkDelete_Click">Xoá bỏ </asp:LinkButton></li></ul>
       <cc1:ConfirmButtonExtender ID="confirmDel" runat="server" TargetControlID="imgDel"
            ConfirmText="Bạn có muốn xóa?">
        </cc1:ConfirmButtonExtender>
        <cc1:ConfirmButtonExtender ID="confirmDel2" runat="server" TargetControlID="linkDelete"
            ConfirmText="Bạn có muốn xóa?">
        </cc1:ConfirmButtonExtender>
        <!-- #ul:toolbox -->
    </div>
    <!-- #div:module-top -->
    <div id="module-content">
        <div id="module-tile-area">
            <ul id="module-title">
                <li>
                    <img src="images/ad_title_bullet.gif" /></li><li id="module-title-text">&nbsp;<asp:Label
                        id="lblTitle" runat="server"></asp:Label></li></ul>
            <div id="dropdown-area">
            </div>
        </div>
        <!-- #div:module-tile-area -->
        <div id="content-area">
            <hr class="admin-hr" />
            <uc2:MessageView ID="MessageView1" runat="server" />
            <uc1:MesssageList ID="MesssageList1" runat="server" OnDetailsClick="ViewDetails_Click" />
            
        </div>
    </div>
    <!-- #div:module-content -->
    <div id="module-bottom">
    </div>
    <!-- #div:module-bottom -->
</asp:Content>
