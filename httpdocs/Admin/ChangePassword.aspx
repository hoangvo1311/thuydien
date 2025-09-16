<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword"%>
<asp:Content ID="ct_main" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="panel panel-primary">
        <div class="panel-heading">Đổi mật khẩu</div>
        <div class="panel-body">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr style="height:35px;">
                    <td style="width:250px;text-align:left;text-indent:20px;">
                        <label for="tb_username">Tên đăng nhập :</label>
                    </td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="tb_username" runat="server" CssClass="TextboxStyle form-control" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height:35px;">
                    <td style="width:250px;text-align:left;text-indent:20px;">
                         <label for="tb_oldpass">Mật khẩu cũ :</label>
                    </td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="tb_oldpass" runat="server" placeholder="Nhập vào mật khẩu hiện tại đang dùng" TextMode="Password" CssClass="TextboxStyle form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height:35px;">
                    <td style="width:250px;text-align:left;text-indent:20px;">
                         <label for="tb_newpass">Mật khẩu mới :</label>
                    </td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="tb_newpass" runat="server" placeholder="Nhập vào mật khẩu mới" TextMode="Password" CssClass="TextboxStyle form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_newpass" runat="server" ControlToValidate="tb_newpass" ErrorMessage="Mật khẩu phải khác rỗng !"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="height:35px;">
                    <td style="width:250px;text-align:left;text-indent:20px;">
                         <label for="tb_confirm">Xác nhận lại mật khẩu mới :</label>
                    </td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="tb_confirm" runat="server" placeholder="Nhập vào mật khẩu mới 1 lần nữa" TextMode="Password" CssClass="TextboxStyle form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_confirm" runat="server" ControlToValidate="tb_confirm"
                            ErrorMessage="Mật khẩu phải khác rỗng !"></asp:RequiredFieldValidator></td>
                </tr>
                <tr style="height:35px;">
                    <td style="width:250px;text-align:left;text-indent:20px;">
                    </td>
                    <td style="text-align:left;">
                        <br />
                        <asp:Button ID="bt_save" runat="server" Text="Đổi mật khẩu" CssClass="ButtonStyle btn btn-primary" OnClick="bt_save_Click" />
                        <asp:Button ID="bt_cancel" runat="server" Text="Hủy bỏ" CssClass="ButtonStyle btn btn-default" CausesValidation="False" OnClick="bt_cancel_Click" />
                        <asp:CustomValidator ID="cst_valid" runat="server" OnServerValidate="cst_valid_ServerValidate"></asp:CustomValidator></td>
                </tr>
                <tr style="height:35px;">
                    <td style="width:250px;text-align:left;text-indent:20px;">
                    </td>
                    <td style="text-align:left;">
                        <asp:Label ID="lb_stt" runat="server"></asp:Label></td>
                </tr>
                <tr style="height:35px;">
                    <td style="width:250px;text-align:left;text-indent:20px;">
                    </td>
                    <td style="text-align:left;">
                        <asp:CompareValidator ID="cv_pass" runat="server" ControlToCompare="tb_newpass" ControlToValidate="tb_confirm"
                            ErrorMessage="Mật khẩu mới và Xác nhận mật khẩu không trùng khớp !" SetFocusOnError="True"></asp:CompareValidator></td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>

