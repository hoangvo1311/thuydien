<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AlbumAddNew.ascx.cs" Inherits="Admin_Modules_Gallery_AlbumAddNew" %>

<table style="width: 100%;" class="table table-condensed">
    <tr>
        <td style="width:200px">
            Tên Album <span style="color: red">(*)</span></td>
        <td>
            <asp:TextBox ID="txtAlbumName" placeholder="Nhập vào tên album ảnh" runat="server" CssClass="form-control"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            Mô tả</td>
        <td>
            <asp:TextBox ID="txtDescription" placeholder="Nhập vào mô tả cho album ảnh hoặc có thể để trống" runat="server" Height="114px" MaxLength="500" Rows="5"
                TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
    </tr>
    <tr>
        <td >
            Xuất bản</td>
        <td>
            <asp:DropDownList ID="ddlPublished" runat="server" CssClass="form-control">
                <asp:ListItem Selected="True" Value="True">Xuất bản</asp:ListItem>
                <asp:ListItem Value="False">Kh&#244;ng</asp:ListItem>
            </asp:DropDownList></td>
    </tr>
</table>
