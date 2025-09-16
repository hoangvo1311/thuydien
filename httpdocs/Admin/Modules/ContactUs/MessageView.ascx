<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageView.ascx.cs" Inherits="Admin_Modules_ContactUs_MessageView" %>
<table width="100%" class="table">
    <tr>
        <td style="width: 100px" valign="top">
            Họ tên</td>
        <td>
            <asp:TextBox ID="txtName" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 100px" valign="top">
            Email</td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 100px" valign="top">
            IP</td>
        <td>
            <asp:TextBox ID="txtIP" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 100px" valign="top">
            Subject</td>
        <td>
            <asp:TextBox ID="txtSubject" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 100px" valign="top">
            Content</td>
        <td>
            <asp:TextBox ID="txtContent" runat="server" Height="116px" ReadOnly="True" TextMode="MultiLine"
                 CssClass="form-control"></asp:TextBox></td>
    </tr>
</table>
