<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticleAddnew.ascx.cs"
    Inherits="Admin_Modules_Articles_ArticleAddnew" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:HiddenField ID="hdArticleID" runat="server" Value="0" />
<asp:ValidationSummary ID="vldSum" runat="server" DisplayMode="List" EnableViewState="False"
    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Article" />
<table style="width: 100%;" class="table table-condensed">
    <tr>
        <td style="width: 150px; height: 26px">
            Tiêu đề <span style="color: red"> (*)</span></td>
        <td style="height: 26px">
            <asp:TextBox ID="txtTitle" MaxLength="256" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqTitle" runat="server" ErrorMessage="Bạn chưa nhập tiêu đề" ControlToValidate="txtTitle" Display="None" ValidationGroup="Article"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 150px;">
            Phê chuẩn</td>
        <td>
            <asp:DropDownList ID="ddlApproved" runat="server" CssClass="form-control">
                <asp:ListItem Text="Kh&#244;ng" Value="No"></asp:ListItem>
                <asp:ListItem Text="C&#243;" Value="Yes"  Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 150px;">
            Xuất bản</td>
        <td>
            <asp:DropDownList ID="ddlPublished" runat="server" CssClass="form-control">
                <asp:ListItem Text="Kh&#244;ng" Value="No"></asp:ListItem>
                <asp:ListItem Text="C&#243;" Value="Yes" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 150px">
            Tóm tắt <span style="color: red"> (*)</span>
        </td>
        <td>
            <fckeditorv2:fckeditor id="txtAbstract" runat="server" SkinPath="skins/silver/" BasePath="~/public_controls/fckeditor/"
                ToolbarSet="MyToolbar"></fckeditorv2:fckeditor>
        </td>
    </tr>
    <tr>
        <td style="width: 150px">
            Nội dung <span style="color: red"> (*)</span>
        </td>
        <td>
            <fckeditorv2:fckeditor id="txtBody" runat="server" SkinPath="skins/silver/" BasePath="~/public_controls/fckeditor/"
                ToolbarSet="MyToolbar" Height="600px"></fckeditorv2:fckeditor>
        </td>
    </tr>
</table>
