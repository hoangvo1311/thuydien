<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageView.ascx.cs" Inherits="Admin_Modules_Suggest_MessageView" %>
<table>
    <tr>
        <td style="width: 100px" valign="top">
            Họ tên</td>
        <td style="width: 100px">
            <asp:TextBox ID="txtName" runat="server" ReadOnly="True" Width="450px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 100px" valign="top">
            Email</td>
        <td style="width: 100px">
            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True" Width="450px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 100px" valign="top">
            IP</td>
        <td style="width: 100px">
            <asp:TextBox ID="txtIP" runat="server" ReadOnly="True" Width="450px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 100px" valign="top">
            Subject</td>
        <td style="width: 100px">
            <asp:TextBox ID="txtSubject" runat="server" ReadOnly="True" Width="450px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 100px" valign="top">
            Content</td>
        <td style="width: 100px">
            <asp:TextBox ID="txtContent" runat="server" Height="216px" ReadOnly="True" TextMode="MultiLine"
                Width="450px"></asp:TextBox></td>
    </tr>
</table>
