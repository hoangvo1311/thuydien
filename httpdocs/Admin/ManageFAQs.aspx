<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true"
    CodeFile="ManageFAQs.aspx.cs" Inherits="Admin_ManageFAQs" Title="" ValidateRequest="false"%>

<%@ Register Src="Modules/FAQs/FAQList.ascx" TagName="FAQList" TagPrefix="uc1" %>
<%@ Register Src="Modules/FAQs/FAQAddNew.ascx" TagName="FAQAddNew" TagPrefix="uc2" %>
<%@ Register Src="Modules/FAQs/FAQUpdate.ascx" TagName="FAQUpdate" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading"><asp:Label id="lblTitle" runat="server"></asp:Label></div>
        <div class="panel-body">
            <ul id="toolbox">
                <li>
                    <asp:ImageButton ID="imgHome" runat="server" ImageUrl="~/Admin/images/ad_icon_home.gif"
                        OnClick="imgHome_Click" />
                    <br />
                    <asp:LinkButton ID="linkHome" runat="server" CssClass="toolbox-link" OnClick="linkHome_Click">Trang chủ</asp:LinkButton>
                </li>
                <li>
                    <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/Admin/images/ad_icon_save.gif"
                        OnClick="imgSave_Click" ValidationGroup="Product" />
                    <br />
                    <asp:LinkButton ID="linkSave" runat="server" CssClass="toolbox-link" OnClick="linkSave_Click"
                        ValidationGroup="Product">Lưu</asp:LinkButton>
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
                    <asp:LinkButton ID="linkEdit" runat="server" CssClass="toolbox-link" OnClick="linkEdit_Click">Sửa đổi</asp:LinkButton></li><li
                        id="icon-del">
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


            <hr class="admin-hr hidden" />
            <uc2:FAQAddNew ID="FAQAddNew1" runat="server" />
            <uc3:FAQUpdate ID="FAQUpdate1" runat="server" />
            <uc1:FAQList ID="FAQList1" runat="server" OnDetailsClick="ViewDetails_Click"/>


        </div>
    </div>
</asp:Content>
