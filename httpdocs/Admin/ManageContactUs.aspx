<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true"
    CodeFile="ManageContactUs.aspx.cs" Inherits="Admin_ManageContactUs" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="Modules/ContactUs/MesssageList.ascx" TagName="MesssageList" TagPrefix="uc1" %>
<%@ Register Src="Modules/ContactUs/MessageView.ascx" TagName="MessageView" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading"><asp:Label id="lblTitle" runat="server"></asp:Label></div>
        <div class="panel-body">
        <ul id="toolbox">
            <li>
                <asp:ImageButton ID="imgHome" runat="server" ImageUrl="~/Admin/images/home_32.png" OnClick="imgHome_Click" />
                <br />
                <asp:LinkButton ID="linkHome" runat="server" CssClass="toolbox-link" OnClick="linkHome_Click">Trang chủ</asp:LinkButton>
            </li>
            <li id="icon-close">
                <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/Admin/images/disable_block_32.png"
                    OnClick="imgClose_Click" />
                <br />
                <asp:LinkButton ID="linkClose" runat="server" CssClass="toolbox-link" OnClick="linkClose_Click">Đóng lại</asp:LinkButton>
            </li>
            <li id="icon-del">
                <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Admin/images/delete_32.png"
                    OnClick="imgDel_Click" />
                <br />
                <asp:LinkButton ID="linkDelete" runat="server" CssClass="toolbox-link" OnClick="linkDelete_Click">Xoá bỏ</asp:LinkButton></li>
        </ul>
       <cc1:ConfirmButtonExtender ID="confirmDel" runat="server" TargetControlID="imgDel"
            ConfirmText="Bạn có muốn xóa?">
        </cc1:ConfirmButtonExtender>
        <cc1:ConfirmButtonExtender ID="confirmDel2" runat="server" TargetControlID="linkDelete"
            ConfirmText="Bạn có muốn xóa?">
        </cc1:ConfirmButtonExtender>


            <uc2:MessageView ID="MessageView1" runat="server" />
            <uc1:MesssageList ID="MesssageList1" runat="server" OnDetailsClick="ViewDetails_Click" />


        </div>
    </div>



    
</asp:Content>
