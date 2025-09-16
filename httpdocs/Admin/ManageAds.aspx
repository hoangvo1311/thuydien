<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true"
    CodeFile="ManageAds.aspx.cs" Inherits="Admin_ManageAds" Title="" %>

<%@ Register Src="Modules/Ads/AdsAddNew.ascx" TagName="AdsAddNew" TagPrefix="uc1" %>
<%@ Register Src="Modules/Ads/AdsUpdate.ascx" TagName="AddUpdate" TagPrefix="uc2" %>
<%@ Register Src="Modules/Ads/AdsList.ascx" TagName="AdsList" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading"><asp:Label id="lblTitle" runat="server"></asp:Label></div>
        <div class="panel-body">
            <ul id="toolbox">
                <li>
                    <asp:ImageButton ID="imgHome" runat="server" ImageUrl="~/Admin/images/home_32.png"
                        OnClick="imgHome_Click" />
                    <br />
                    <asp:LinkButton ID="linkHome" runat="server" CssClass="toolbox-link" OnClick="linkHome_Click">Trang chủ</asp:LinkButton>
                </li>
                <li>
                    <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/Admin/images/save_32.png"
                        OnClick="imgSave_Click" ValidationGroup="Product" />
                    <br />
                    <asp:LinkButton ID="linkSave" runat="server" CssClass="toolbox-link" OnClick="linkSave_Click"
                        ValidationGroup="Product">Lưu</asp:LinkButton>
                </li>
                <li id="icon-close">
                    <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/Admin/images/disable_block_32.png"
                        OnClick="imgClose_Click" CausesValidation="False" />
                    <br />
                    <asp:LinkButton ID="linkClose" runat="server" CssClass="toolbox-link" OnClick="linkClose_Click" CausesValidation="False">Đóng lại</asp:LinkButton>
                </li>
                <li id="icon-add">
                    <asp:ImageButton ID="imgAdd" runat="server" ImageUrl="~/Admin/images/plus_32.png"
                        OnClick="imgAdd_Click" />
                    <br />
                    <asp:LinkButton ID="linkAdd" runat="server" CssClass="toolbox-link" OnClick="linkAdd_Click">Thêm mới</asp:LinkButton>
                </li>
                <li>
                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Admin/images/pencil_32.png"
                        OnClick="imgEdit_Click" />
                    <br />
                    <asp:LinkButton ID="linkEdit" runat="server" CssClass="toolbox-link" OnClick="linkEdit_Click">Sửa đổi</asp:LinkButton></li><li
                        id="icon-del">
                        <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Admin/images/delete_32.png"
                            OnClick="imgDel_Click" CausesValidation="False" />
                        <br />
                        <asp:LinkButton ID="linkDelete" runat="server" CssClass="toolbox-link" OnClick="linkDelete_Click" CausesValidation="False">Xoá bỏ</asp:LinkButton></li></ul>
            <cc1:ConfirmButtonExtender ID="confirmDel" runat="server" TargetControlID="imgDel"
                ConfirmText="Bạn có muốn xóa?">
            </cc1:ConfirmButtonExtender>
            <cc1:ConfirmButtonExtender ID="confirmDel2" runat="server" TargetControlID="linkDelete"
                ConfirmText="Bạn có muốn xóa?">
            </cc1:ConfirmButtonExtender>
            <!-- #ul:toolbox -->

            <hr class="admin-hr hidden" />
            <uc1:AdsAddNew ID="AdsAddNew1" runat="server" />
            <uc2:AddUpdate ID="AdsUpdate1" runat="server" />
            <uc3:AdsList id="AdsList1" runat="server" OnDetailsClick="ViewDetails_Click">
            </uc3:AdsList>

        </div>
    </div>
    
</asp:Content>
