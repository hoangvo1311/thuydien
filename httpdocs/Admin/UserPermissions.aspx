<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true" CodeFile="UserPermissions.aspx.cs" Inherits="UserPermissions" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading"><span class="icon icon-back"></span><a href="Grouplist.aspx">Về trước</a> | Cấp phát quyền cho người sử dụng</div>
        <div class="panel-body">
<table style="width: 100%">        
        <tr>
            <td colspan="3">
                Tên đăng nhập: <asp:Label ID="lblUserID" Font-Bold="true" runat="server" Width="114px"></asp:Label>                
                Họ và tên: <asp:Label ID="lblUserName" Font-Bold="true" runat="server" Width="200px"></asp:Label>
                <hr/>
            </td>
        </tr>        
        <tr>
            <td colspan="3">
                <strong>Cấp phát quyền cho người sử dụng theo nhóm</strong>
            </td>
        </tr>
        <tr>
            <td width="40%" valign="top">&nbsp;&nbsp;<b>Danh sách nhóm hiện có</b></td>
            <td width="20%" valign="top">
            </td>
            <td width="40%" valign="top">&nbsp;&nbsp;<b>Danh sách nhóm trực thuộc</b>
            </td>
        </tr>                
        <tr>
            <td width="40%" valign="top">&nbsp;
                <asp:ListBox ID="lstGroups" runat="server" Width="90%" Height="150px" CssClass="NormalListBox"></asp:ListBox></td>
            <td width="20%" valign="top" align="center">
                <asp:Button ID="butAddGroup" runat="server" 
                    Text="Bổ sung" Width="97px" CssClass="NormalButton" OnClick="butAddGroup_Click" />
                <br />
                <br />
                <asp:Button ID="butRemoveGroup" runat="server" 
                    Text="Loại bỏ" Width="98px" CssClass="NormalButton" OnClick="butRemoveGroup_Click" /></td>
            <td width="40%" valign="top">&nbsp;
                <asp:ListBox ID="lstPermitedGroups" runat="server" Width="90%" Height="150px" CssClass="NormalListBox"></asp:ListBox></td>
        </tr>        
    </table>
        </div>
    </div>


</asp:Content>

