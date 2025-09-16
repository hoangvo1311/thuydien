<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Photo.ascx.cs" Inherits="Admin_Modules_Gallery_Photo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<hr class="admin-hr" />
<asp:Label ID="lblPhotoIndex" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnPre" runat="server" Text="Trước" OnClick="btnPre_Click" />
<asp:Button ID="btnNext" runat="server" Text="Sau" OnClick="btnNext_Click" /><br /><br />
<asp:Image ID="imgPhoto" runat="server" />
<br /><br />
<asp:Label ID="Label1" runat="server" Text="Nhập mô tả cho ảnh:"></asp:Label><br />
<asp:TextBox ID="txtDescription" runat="server" Height="50px" MaxLength="500" TextMode="MultiLine" Width="600px"></asp:TextBox>
