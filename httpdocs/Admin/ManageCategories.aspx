<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageCategories.aspx.cs" Inherits="Admin_ManageCategories" MasterPageFile="~/Admin/AdminVTT.master"%>

<%@ Register Src="Modules/Categories/CategoriesHome.ascx" TagName="CategoriesHome"
    TagPrefix="uc4" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="Modules/Categories/CategoryUpdate.ascx" TagName="CategoryUpdate"
    TagPrefix="uc3" %>

<%@ Register Src="Modules/Categories/CategoryAddnew.ascx" TagName="CategoryAddnew"
    TagPrefix="uc2" %>

<%@ Register Src="Modules/Categories/CategoriesList.ascx" TagName="CategoriesList"
    TagPrefix="uc1" %>
    
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
                    <asp:ImageButton ID="imgHomeCategories" ToolTip="Cấu hình chuyên mục hiển thị tại trang chủ" runat="server" ImageUrl="~/Admin/images/confg_24.gif" OnClick="imgHomeCategories_Click" />
                    <br />
                    <asp:LinkButton ID="linkHomeCategories" ToolTip="Cấu hình chuyên mục hiển thị tại trang chủ" runat="server" CssClass="toolbox-link" OnClick="linkHomeCategories_Click">Cấu hình</asp:LinkButton>
                </li>
                <li>
                    <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/Admin/images/ad_icon_save.gif" OnClick="imgSave_Click" />
                    <br />
                    <asp:LinkButton ID="linkSave" runat="server" CssClass="toolbox-link" OnClick="linkSave_Click">Lưu</asp:LinkButton>
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

        <div id="content-area">
            <hr class="admin-hr hidden" />
            <uc1:CategoriesList ID="CategoriesList1" runat="server" />
            <uc2:CategoryAddnew ID="CategoryAddnew1" runat="server" Visible="false" />
            
            <uc3:CategoryUpdate ID="CategoryUpdate1" runat="server" Visible="false" />
            <uc4:CategoriesHome ID="CategoriesHome1" runat="server" Visible="false"  />
        </div>

        </div>
    </div>

</asp:Content>
