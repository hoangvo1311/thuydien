<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdsUpdate.ascx.cs" Inherits="Admin_Modules_Ads_AdsUpdate" %>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
    EnableViewState="False" ShowMessageBox="True" ShowSummary="False" />
<%@ Register Src="~/public_controls/wcDateTimePicker.ascx" TagName="wcDateTimePicker"
    TagPrefix="uc1" %>

<table style="width: 100%" class="table table-condensed">
    <tr>
        <td style="width:200px">
            Tên quảng cáo</td>
        <td>
            <asp:TextBox ID="txtLinkName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqLinkName" runat="server" ControlToValidate="txtLinkName"
                Display="None" EnableViewState="False" ErrorMessage="Tên quảng cáo không được trống"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td>
            Ảnh quảng cáo</td>
        <td>
            <asp:TextBox ID="txtAdsImage" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnBrowser" runat="server" Text="Chọn ảnh" /></td>
        <asp:RequiredFieldValidator ID="rqAdsImage" runat="server" ControlToValidate="txtLinkName"
            Display="None" ErrorMessage="Ảnh quảng cáo không được trống" EnableViewState="False"
            ValidationGroup="Ads"></asp:RequiredFieldValidator></tr>
    <tr>
    <tr>
        <td>
            Liên kết đến</td>
        <td>
            <asp:TextBox ID="txtJumpURL" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqJumpURL" runat="server" ControlToValidate="txtJumpURL"
                Display="None" EnableViewState="False" ErrorMessage="Liên kết không được trống"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td>
            Thứ tự</td>
        <td>
            <asp:TextBox ID="txtSortOrder" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqSortOrder" runat="server" ControlToValidate="txtSortOrder"
                Display="None" EnableViewState="False" ErrorMessage="Thứ tự không được trống"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td>
            Vị trí
        </td>
        <td>
            <asp:DropDownList ID="ddlAdsPosition" runat="server" CssClass="form-control">
                <asp:ListItem Value="main" Selected="True">Ở giữa - Trang chủ</asp:ListItem>
                <asp:ListItem Value="donvitructhuoc">Đơn vị trực thuộc</asp:ListItem>

                <asp:ListItem Value="left">B&#234;n trái</asp:ListItem>
                <asp:ListItem Value="right">B&#234;n phải</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <%-- <tr>
        <td style="width: 100px">
            Ngày bắt đầu</td>
        <td>
            <uc1:wcDateTimePicker ID="dtpStartDate" runat="server"></uc1:wcDateTimePicker>
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
            Ngày kết thúc</td>
        <td>
            <uc1:wcDateTimePicker ID="dtpEndDate" runat="server"></uc1:wcDateTimePicker>
        </td>
    </tr>--%>
    <tr>
        <td>
            Hiển thị</td>
        <td>
            <asp:DropDownList ID="ddlEnabled" runat="server" CssClass="form-control">
                <asp:ListItem Text="Có" Value="true"></asp:ListItem>
                <asp:ListItem Text="Không" Value="false"></asp:ListItem>
            </asp:DropDownList></td>
    </tr>
</table>
