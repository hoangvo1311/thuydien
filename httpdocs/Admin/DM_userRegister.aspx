<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true" CodeFile="DM_userRegister.aspx.cs" Inherits="Admin_DM_userRegister" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="panel panel-primary">
        <div class="panel-heading"><span class="icon icon-back"></span><a href="ManagePermission.aspx">Về trước</a> | Tạo mới người sử dụng</div>
        <div class="panel-body">
<table style="width:100%">
        <tr>
            <td colspan="2" valign="top" align="right">
                <b>Đăng ký người sử dụng cho đơn vị:&nbsp;</b>
                <asp:DropDownList ID="cmbDonvi" runat="server" Width="280px">
                </asp:DropDownList>
                <hr />
            </td>
        </tr>        
        <tr>
            <td colspan="2">
                <strong>Thông tin chung</strong></td>
        </tr>
        <tr>
            <td class="TDInputLeft" nowrap="noWrap" width="200">
                Tên đăng nhập (*):</td>
            <td class="TDInputRight">
                <asp:TextBox ID="txtTen_User" placeholder="username" runat="server" CssClass="NormalTextBox form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTen_User"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:Label ID="lblErrUserID" runat="server" ForeColor="Red" Text="Tên đăng nhập đã sử dụng!"
                    Visible="False" Width="195px"></asp:Label></td>
        </tr>
        <tr>
            <td class="TDInputLeft" nowrap="noWrap">
                Họ tên user (*):</td>
            <td class="TDInputRight">
                <asp:TextBox ID="txtLastName" placeholder="Nhập họ và tên đầy đủ" runat="server" CssClass="NormalTextBox form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                    ErrorMessage="*"></asp:RequiredFieldValidator>                
            </td>
        </tr>
        <tr>
            <td class="TDInputLeft" nowrap="noWrap" style="height: 25px">
                Số điện thoại:</td>
            <td class="TDInputRight" style="height: 25px">
                <asp:TextBox ID="txtPhoneNumber" placeholder="Có thể nhập hoặc không" runat="server" CssClass="NormalTextBox form-control"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <strong>Mật khẩu truy cập:</strong></td>
        </tr>
        <tr>
            <td class="TDInputLeft" nowrap="noWrap">
                Mật khẩu (*):</td>
            <td class="TDInputRight">
                <asp:TextBox ID="txtPassword" placeholder="Mật khẩu 6 ký tự trở lên" runat="server" CssClass="NormalTextBox form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="TDInputLeft" nowrap="noWrap">
                Nhập lại mật khẩu (*):</td>
            <td class="TDInputRight">
                <asp:TextBox ID="txtRePassword" placeholder="Mật khẩu 6 ký tự trở lên" runat="server" CssClass="NormalTextBox form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRePassword"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Text="Mật khẩu không trùng khớp!"
                    Visible="False" Width="180px"></asp:Label></td>
        </tr>
        <tr>
            <td class="TDInputLeft">
            </td>
            <td class="TDInputRight">
                <asp:Button ID="butInput" runat="server" CssClass="NormalButton btn btn-primary" Text="Đăng ký" OnClick="butInput_Click" /></td>
        </tr>
    </table>
        </div>
    </div>



</asp:Content>

