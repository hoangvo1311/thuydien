<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticlesConfig.ascx.cs" Inherits="Admin_Modules_Categories_ArticlesConfig" %>
<table style="width: 100%" class="table table-condensed">
    <tr>
        <td colspan="2"><label>CẤU HÌNH TRANG CHỦ</label></td>
    </tr>
    <tr style="display:none;">
        <td style="width:200px">
            Tin tức sự kiện mới nhất</td>
        <td>
            <asp:DropDownList ID="ddlCategories_Left" runat="server" CssClass="form-control">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            Lịch công tác tuần</td>
        <td>
            <asp:DropDownList ID="ddlCategories_Center" runat="server" CssClass="form-control">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            Văn bản mới</td>
        <td>
            <asp:DropDownList ID="ddlCategories_Right" runat="server" CssClass="form-control">
            </asp:DropDownList></td>
    </tr>
</table>
