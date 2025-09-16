<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LinkUpdate.ascx.cs" Inherits="Admin_Modules_Links_LinkUpdate" %>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
    EnableViewState="False" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Link" />
<table style="width: 100%">
    <tr>
        <td style="width: 150px; height: 36px" valign="top">
            Tên liên kết <span style="color: red">(*)</span></td>
        <td style="height: 36px">
            <asp:TextBox ID="txtLinkName" runat="server" MaxLength="256" ValidationGroup="Link" CssClass="form-control"
                ></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqLinkName" runat="server" ControlToValidate="txtLinkName"
                Display="None" ErrorMessage="Bạn chưa nhập tên liên kết" SetFocusOnError="True"
                ValidationGroup="Link"></asp:RequiredFieldValidator></td>
    </tr>
    <tr style="color: #000000">
        <td style="width: 150px" valign="top">
            Đường dẫn (URL)</td>
        <td>
            <asp:TextBox ID="txtURL" runat="server" MaxLength="256" CssClass="form-control"></asp:TextBox></td>
    </tr>
</table>
