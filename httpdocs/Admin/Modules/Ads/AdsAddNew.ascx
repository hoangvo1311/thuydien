<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdsAddNew.ascx.cs" Inherits="Admin_Modules_Ads_AdsAddNew" %>
<%@ Register Src="~/public_controls/wcDateTimePicker.ascx" TagName="wcDateTimePicker"
    TagPrefix="uc1" %>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
    EnableViewState="False" ShowMessageBox="True" ShowSummary="False" />
<table style="width: 100%" class="table table-condensed">
    <tr>
        <td style="width:200px">
            Tên quảng cáo</td>
        <td>
            <asp:TextBox placeholder="Nhập vào tên quảng cáo" ID="txtLinkName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqLinkName" runat="server" ControlToValidate="txtLinkName"
                Display="None" ErrorMessage="Tên quảng cáo không được trống" EnableViewState="False"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td>
            Ảnh quảng cáo</td>
        <td>
            <asp:TextBox ID="txtAdsImage" placeholder="Chọn ảnh quảng cáo trên máy chủ" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnBrowser" runat="server" Text="Chọn ảnh" /></td>
        <asp:RequiredFieldValidator ID="rqAdsImage" runat="server" ControlToValidate="txtLinkName"
            Display="None" ErrorMessage="Ảnh quảng cáo không được trống" EnableViewState="False"
            ValidationGroup="Ads"></asp:RequiredFieldValidator></tr>
    <tr>
        <td>
            Liên kết đến</td>
        <td>
            <asp:TextBox ID="txtJumpURL" placeholder="Nhập vào địa chỉ website cần chuyển đến" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqJumpURL" runat="server" ControlToValidate="txtJumpURL"
                Display="None" ErrorMessage="Liên kết không được trống" EnableViewState="False"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td>
            Thứ tự</td>
        <td>
            <asp:TextBox ID="txtSortOrder" placeholder="Nhập thứ tự hiển thị từ trên xuống dưới 1, 2, 3..." runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqSortOrder" runat="server" ControlToValidate="txtSortOrder"
                Display="None" ErrorMessage="Thứ tự không được trống" EnableViewState="False"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td>
            Vị trí
        </td>
        <td>
            <asp:DropDownList ID="ddlAdsPosition" runat="server" CssClass="form-control">
                <asp:ListItem Value="main" Selected="True">Ở giữa - Trang chủ</asp:ListItem>
                <%--<asp:ListItem Value="donvitructhuoc">Đơn vị trực thuộc</asp:ListItem>--%>
                <asp:ListItem Value="left" >B&#234;n trái</asp:ListItem>
                <asp:ListItem Value="right" >B&#234;n phải</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
    <%--    <td style="width: 100px">
            Ngày bắt đầu</td>
        <td>
            <uc1:wcDateTimePicker ID="dtpStartDate" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
            Ngày kết thúc</td>
        <td>
            <uc1:wcDateTimePicker ID="dtpEndDate" runat="server" />
        </td>
    </tr>--%>
    <tr>
        <td>
            Hiển thị</td>
        <td>
            <asp:DropDownList ID="ddlEnabled" runat="server" CssClass="form-control">
                <asp:ListItem Text="Có" Value="true" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Không" Value="false"></asp:ListItem>
            </asp:DropDownList></td>
    </tr>
</table>
