<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TypeProductAddnew.ascx.cs" Inherits="Admin_Modules_Products_TypeProductAddnew" %>
<asp:HiddenField ID="hdProductTypeID" runat="server" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductTypeName"
    Display="None" ErrorMessage="Tên loại hàng không được trống"></asp:RequiredFieldValidator>
<table style="width: 100%">
    <tr>
        <td style="width: 100px">
            Tên loại hàng</td>
        <td style="width: 100px">
            <asp:TextBox ID="txtProductTypeName" runat="server" MaxLength="255" Width="400px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 100px">
            Mô tả</td>
        <td style="width: 100px">
            <asp:TextBox ID="txtDescription" runat="server" Rows="2" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
    </tr>
</table>
