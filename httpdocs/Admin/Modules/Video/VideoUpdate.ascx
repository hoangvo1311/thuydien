<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideoUpdate.ascx.cs" Inherits="Admin_Modules_Video_VideoUpdate" %>
<table style="width: 100%" class="table table-condensed">
    <tr>
        <td style="width: 150px; " valign="top">
            Tiêu đề <span style="color: #ff0000">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                    ErrorMessage="(*)" ValidationGroup="Video"></asp:RequiredFieldValidator></span></td>
        <td>
            <asp:TextBox ID="txtTitle" runat="server" MaxLength="256" ValidationGroup="Video"
                CssClass="form-control" CausesValidation="True"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr style="color: #000000">
        <td style="width: 150px" valign="top">
            Đường dẫn
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFile"
                ErrorMessage="(*)" ValidationGroup="Video"></asp:RequiredFieldValidator><span style="color: red"></span></td>
        <td>
            <asp:Label ID="lblFile" runat="server" Text="http://www.youtube.com/embed/"></asp:Label>
            <asp:TextBox ID="txtFile" runat="server" CausesValidation="True" MaxLength="256"
                ValidationGroup="Video" Width="334px"></asp:TextBox>
            <!--
            &nbsp;<asp:Button ID="btnBrowser" runat="server" Text="Chọn file" />
                -->
        </td>
    </tr>
</table>