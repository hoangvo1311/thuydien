<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailToFriends.aspx.cs" Inherits="EmailToFriends" %>

<%@ Register Assembly="NatsNet.Web.UI.Controls" Namespace="NatsNet.Web.UI.Controls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= MMC.VTT.Properties.WEB_CONFIG.ApplicationName %> - Email To Friends</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0">
                <tr>
                    <td>
                        Tên của bạn:</td>
                    <td align="right">
                        <asp:TextBox ID="txtName" runat="server" MaxLength="30" Width="300px"></asp:TextBox>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Email của bạn:</td>
                    <td align="right">
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" Width="300px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                            Display="None" ErrorMessage="Email của bạn không chính xác" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Gửi đến (To):</td>
                    <td align="right">
                        <asp:TextBox ID="txtTo" runat="server" MaxLength="30" Width="300px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTo"
                            Display="None" ErrorMessage="Email gửi đến không chính xác" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Đồng gửi đến (CC):</td>
                    <td align="right">
                        <asp:TextBox ID="txtCC" runat="server" MaxLength="30" Width="300px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCC"
                            Display="None" ErrorMessage="Email đồng gửi đến không chính xác" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tiêu đề (Subject):</td>
                    <td align="right">
                        <asp:TextBox ID="txtSubject" runat="server" MaxLength="255" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Thông điệp (Message):
                    </td>
                    <td align="right">
                        <asp:TextBox ID="txtMessage" runat="server" MaxLength="30" Rows="5" Width="300px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                    </td>
                    <td align="left">
                        <cc1:ImageVerifier ID="ImageVerifier1" runat="server" /><br />
                        <asp:TextBox ID="txtVerifyText" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td valign="top">
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSend" runat="server" Text="Gửi" OnClick="btnSend_Click" Width="80px" />
                        <asp:Button ID="btnCancel" runat="server" Text="Bỏ qua" Width="80px" OnClientClick="window.parent.close();" /></td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
        </div>
    </form>
</body>
</html>
