<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true" CodeFile="ManageDonation.aspx.cs" Inherits="Admin_ManageDonation" ValidateRequest="false"%>

<%@ Register Src="Modules/Donation/DonationAddnew.ascx" TagName="DonationAddnew"
    TagPrefix="uc5" %>
<%@ Register Src="Modules/Donation/DonationUpdate.ascx" TagName="DonationUpdate"
    TagPrefix="uc6" %>
<%@ Register Src="Modules/Donation/DonationList.ascx" TagName="DonationList" TagPrefix="uc7" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="module-top">
         <ul id="toolbox" >
            <li>
                <asp:ImageButton ID="imgHome" runat="server" ImageUrl="~/Admin/images/ad_icon_home.gif" OnClick="imgHome_Click" />
                <br />
                <asp:LinkButton ID="linkHome" runat="server" CssClass="toolbox-link" OnClick="linkHome_Click">Trang chủ Quản lý</asp:LinkButton>
            </li>
            <li>
                <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/Admin/images/ad_icon_save.gif" OnClick="imgSave_Click" ValidationGroup="Donation" />
                <br />
                <asp:LinkButton ID="linkSave" runat="server" CssClass="toolbox-link" OnClick="linkSave_Click" ValidationGroup="Donation">Lưu</asp:LinkButton>
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
    </div><!-- #div:module-top -->
    <div id="module-content">
        <div id="module-tile-area">
            <ul id="module-title">
                <li><img src="images/ad_title_bullet.gif" /></li><li id="module-title-text">&nbsp;<asp:Label id="lblTitle" runat="server"></asp:Label></li></ul>
        </div><!-- #div:module-tile-area -->
        <div id="content-area">
            <uc5:DonationAddnew ID="DonationAddnew1" runat="server" />
            <uc6:DonationUpdate ID="DonationUpdate1" runat="server" />
        <uc7:DonationList ID="DonationList1" runat="server" OnDetailsClick="linkEdit_Click" />
        </div>
        
        
    </div><!-- #div:module-content -->
    <div id="module-bottom">
        
        </div><!-- #div:module-bottom -->


</asp:Content>

