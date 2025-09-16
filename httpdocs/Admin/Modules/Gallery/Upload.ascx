<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Upload.ascx.cs" Inherits="Admin_Modules_Gallery_Upload" %>
<label>
Upload ảnh cho Album : <span class="highlight-color larger-text"><asp:Label ID="lblAlbumName" runat="server"></asp:Label></span>
</label><br />
<hr class="admin-hr" />
<asp:FileUpload ID="FileUpload1" runat="server" class="multi" maxlength="5" /><br />
<asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>


