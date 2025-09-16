<%@ Page Language="C#" MasterPageFile="~/Admin/AdminWithoutUpdatePanel.master" AutoEventWireup="true"
    CodeFile="ManageGallery.aspx.cs" Inherits="Admin_ManageGallery" Title="" %>

<%@ Register Src="Modules/Gallery/AlbumUpdate.ascx" TagName="AlbumUpdate" TagPrefix="uc6" %>
<%@ Register Src="Modules/Gallery/AlbumAddNew.ascx" TagName="AlbumAddNew" TagPrefix="uc5" %>
<%@ Register Src="Modules/Gallery/AlbumList.ascx" TagName="AlbumList" TagPrefix="uc1" %>
<%@ Register Src="Modules/Gallery/PhotoList.ascx" TagName="PhotoList" TagPrefix="uc2" %>
<%@ Register Src="Modules/Gallery/Upload.ascx" TagName="Upload" TagPrefix="uc3" %>
<%@ Register Src="Modules/Gallery/Photo.ascx" TagName="Photo" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading"><asp:Label ID="lblTitle" runat="server"></asp:Label></div>
        <div class="panel-body">
            <ul id="toolbox">
                <li>
                    <asp:ImageButton ID="imgHome" runat="server" ImageUrl="~/Admin/images/ad_icon_home.gif"
                        OnClick="imgHome_Click" />
                    <br />
                    <asp:LinkButton ID="linkHome" runat="server" CssClass="toolbox-link" OnClick="linkHome_Click">Trang chủ</asp:LinkButton>
                </li>
                <li>
                    <asp:ImageButton ID="imgUpload" runat="server" ImageUrl="~/Admin/images/exp_24.gif"
                        ValidationGroup="Article" OnClick="imgUpload_Click" />
                    <br />
                    <asp:LinkButton ID="linkUpload" runat="server" CssClass="toolbox-link" ValidationGroup="Article"
                        OnClick="linkUpload_Click">Upload</asp:LinkButton>
                </li>
                <li>
                    <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/Admin/images/ad_icon_save.gif"
                        OnClick="imgSave_Click" ValidationGroup="Article" />
                    <br />
                    <asp:LinkButton ID="linkSave" runat="server" CssClass="toolbox-link" OnClick="linkSave_Click"
                        ValidationGroup="Article">Lưu</asp:LinkButton>
                </li>
                <li id="icon-close">
                    <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/Admin/images/ad_icon_clo.gif"
                        OnClick="imgClose_Click" />
                    <br />
                    <asp:LinkButton ID="linkClose" runat="server" CssClass="toolbox-link" OnClick="linkClose_Click">Đóng lại</asp:LinkButton>
                </li>
                <li id="icon-add">
                    <asp:ImageButton ID="imgAdd" runat="server" ImageUrl="~/Admin/images/ad_icon_add.gif"
                        OnClick="imgAdd_Click" />
                    <br />
                    <asp:LinkButton ID="linkAdd" runat="server" CssClass="toolbox-link" OnClick="linkAdd_Click">Thêm mới</asp:LinkButton>
                </li>
                <li>
                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Admin/images/ad_icon_edit.gif"
                        OnClick="imgEdit_Click" />
                    <br />
                    <asp:LinkButton ID="linkEdit" runat="server" CssClass="toolbox-link" OnClick="linkEdit_Click">Sửa đổi</asp:LinkButton></li>
                <li id="icon-del">
                    <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Admin/images/ad_icon_del.gif"
                        OnClick="imgDel_Click" />
                    <br />
                    <asp:LinkButton ID="linkDelete" runat="server" CssClass="toolbox-link" OnClick="linkDelete_Click">Xoá bỏ </asp:LinkButton></li>
            </ul>
            <cc1:ConfirmButtonExtender ID="confirmDel" runat="server" TargetControlID="imgDel"
                ConfirmText="Bạn có muốn xóa?">
            </cc1:ConfirmButtonExtender>
            <cc1:ConfirmButtonExtender ID="confirmDel2" runat="server" TargetControlID="linkDelete"
                ConfirmText="Bạn có muốn xóa?">
            </cc1:ConfirmButtonExtender>
            <!-- #ul:toolbox -->

        <div id="content-area">
            <hr class="admin-hr hidden" />
            <uc1:AlbumList ID="AlbumList1" runat="server" OnAlbumClick="Album_Click" />
            <uc2:PhotoList ID="PhotoList1" runat="server" OnPhotoClick="PhotoList_Click" />
            <uc4:Photo ID="Photo1" runat="server" />
            <uc3:Upload ID="Upload1" runat="server" />
            <uc5:AlbumAddNew ID="AlbumAddNew1" runat="server" />
            <uc6:AlbumUpdate ID="AlbumUpdate1" runat="server"></uc6:AlbumUpdate>
            <div style="clear:left; width:100%;"></div>
        </div>

        </div>
    </div>


    <!-- #div:module-bottom -->
</asp:Content>
