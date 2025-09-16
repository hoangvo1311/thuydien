<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OldMemberView.ascx.cs" Inherits="Admin_Modules_OldMembers_OldMemberView" %>
<table cellpadding="5" style="width: 100%">
    <tr>
        <td style="width: 121px">
        </td>
        <td>
            <asp:DropDownList ID="ddlMemberType" runat="server" Width="400px" AutoPostBack="True" Enabled="False">
                <asp:ListItem Value="HS">Học sinh</asp:ListItem>
                <asp:ListItem Value="GV">Gi&#225;o vi&#234;n</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 121px">
            Họ tên
        </td>
        <td>
            <asp:TextBox ID="txtFullName" runat="server" Width="400px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 121px">
            Ngày sinh</td>
        <td>
            <asp:TextBox ID="txtBirthDate" runat="server" Width="234px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 121px">
            <asp:Literal ID="lblYear" runat="server" Text="Học tại trường"></asp:Literal></td>
        <td><asp:DropDownList ID="ddlStartYear" runat="server" Width="110px">
        </asp:DropDownList>
            -
            <asp:DropDownList ID="ddlEndYear" runat="server" Width="110px">
            </asp:DropDownList></td>
    </tr>
    <tr id="trSubject" runat="server" visible="false">
        <td style="width: 121px">
            <asp:Literal ID="lblSubject" runat="server" Text="Môn giảng dạy"></asp:Literal></td>
        <td>
            <asp:TextBox ID="txtSubject" runat="server" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td style="width: 121px">
            Nơi công tác</td>
        
        <td>
            <asp:TextBox ID="txtCompany" runat="server" Width="400px"></asp:TextBox></td>
    </tr>
    <tr id="trPosition" runat="server" visible="false">
        <td style="width: 121px">
            Chức vụ hiện tại</td>
        <td>
            <asp:TextBox ID="txtPosition" runat="server" Width="400px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 121px">
            Địa chỉ hiện tại</td>
        <td>
            <asp:TextBox ID="txtAddress" runat="server" Width="400px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 121px">
            Điện thoại</td>
        <td>
            <asp:TextBox ID="txtPhone" runat="server" Width="400px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 121px">
            Email</td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="400px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 121px">
            Thông tin khác</td>
        <td>
            <asp:TextBox ID="txtDescription" runat="server" Width="400px" Height="167px" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
</table>
