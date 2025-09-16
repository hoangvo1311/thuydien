<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductUpdate.ascx.cs" Inherits="Admin_Modules_Products_ProductUpdate" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<asp:HiddenField ID="hdProductID" runat="server" />
<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" style="display:none;" />
<br />
Tên sản phẩm :
<asp:TextBox ID="txtProductName" runat="server" MaxLength="256"></asp:TextBox><br />
Giá sản phẩm :
<asp:TextBox ID="txtCurrentPrice" runat="server" MaxLength="10"></asp:TextBox><br />
Giá khuyến mãi :
<asp:TextBox ID="txtPromotePrice" runat="server" MaxLength="10"> </asp:TextBox><br />
Mô tả :
<FTB:FreeTextBox ID="txtDescription" runat="server" DownLevelCols="50" DownLevelRows="10" Height="150px" ButtonOverImage="True" ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage,InsertRule|Cut,Copy,Paste;Undo,Redo,Printn,NetSpell">
        </FTB:FreeTextBox>
Xuất bản :
<asp:DropDownList ID="ddlPublished" runat="server">
    <asp:ListItem Selected="True" Value="true">Có</asp:ListItem>
    <asp:ListItem Value="false">Không</asp:ListItem>
</asp:DropDownList>
<br />
Ảnh đại diện:
<asp:HiddenField ID="hdLargeImage" runat="server" />
<asp:Image ID="LargeImage" runat="server" /><br />
Hình ảnh :
<asp:Repeater ID="rptImages" runat="server">
    <HeaderTemplate>
        <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li><a target="_blank" href='../images/products/<%# DataBinder.Eval(Container.DataItem, "Name")%>'>
            <img src='../images/products/thumbnails/<%# DataBinder.Eval(Container.DataItem, "Name")%>' /></a>
            <asp:Button ID="btnSetPrimary" runat="server" Text="Ảnh đại diện" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Name") %>'
                OnClick="btnSetPrimary_Click" />
            <asp:Button ID="btnRemove" runat="server" Text="Xóa" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Name") %>'
                OnClick="btnRemove_Click" />
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>
<asp:Button ID="btnSelectImage" runat="server" Text="Chọn ảnh" />
