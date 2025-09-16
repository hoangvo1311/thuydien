<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryAddnew.ascx.cs"
    Inherits="Modules_Categories_CategoryAddnew" %>
<asp:ValidationSummary ID="sumErrors" runat="server" DisplayMode="List" EnableViewState="False"
    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Categories" />
<asp:HiddenField ID="hdCategoryID" runat="server" Value="0" />
<table style="width: 100%" class="table table-condensed">
    <tr>
        <td style="width: 150px; height: 26px;" valign="top">Vị trí menu :</td>
        <td>
            <asp:DropDownList ID="ddlAdsPosition" runat="server" AutoPostBack="True" CssClass="form-control">
                <asp:ListItem Value="top">Ph&#237;a tr&#234;n</asp:ListItem>
                <asp:ListItem Value="left" Selected="True">B&#234;n tr&#225;i</asp:ListItem>
                <asp:ListItem Value="right">B&#234;n phải</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 150px; height: 26px;" valign="top">
            Tên chuyên mục <span style="color: red">(*)</span></td>
        <td style="height: 26px">
            <asp:TextBox ID="txtTitle" runat="server" MaxLength="256" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqTenChuyenMuc" runat="server" ControlToValidate="txtTitle"
                Display="None" ErrorMessage="Bạn chưa nhập tên chuyên mục" SetFocusOnError="True"
                ValidationGroup="Categories"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="width: 150px" valign="top">
            Thuộc chuyên mục</td>
        <td>
            <asp:DropDownList ID="ddlParentCategory" runat="server" CssClass="form-control">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 150px" valign="top">
            Thứ tự</td>
        <td>
            <asp:TextBox ID="txtSortOrder" runat="server" MaxLength="3" CssClass="form-control">0</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 150px" valign="top">
            Mô tả</td>
        <td>
            <asp:TextBox ID="txtDescription" placeholder="Nhập vào mô tả cho chuyên mục hoặc có thể để trống" runat="server" MaxLength="4000" Rows="3" TextMode="MultiLine"
                 CssClass="form-control"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 150px" valign="top">
            Liên kết đến
        </td>
        <td>
            <asp:TextBox ID="txtNavigateURL" placeholder="Để trống hoặc nhập vào liên kết muốn chuyển đến" runat="server" MaxLength="256" CssClass="form-control"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 150px" valign="top">
        </td>
        <td>
            <asp:CheckBox ID="chkEnabled" runat="server" Checked="True" Text="Hiển thị ra phía người dùng" /></td>
    </tr>
</table>
