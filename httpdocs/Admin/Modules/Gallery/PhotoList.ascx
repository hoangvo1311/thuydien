<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PhotoList.ascx.cs" Inherits="Admin_Modules_Gallery_PhotoList" %>

<hr class="admin-hr" />

<label>Album: <span class="highlight-color larger-text"><asp:Label ID="lblAlbumName" runat="server" CssClass="ad-album-name" Text="Label"></asp:Label></span></label>

<%--<asp:Repeater ID="rptPhoto" runat="server">
 <HeaderTemplate>
        <div id="photos">
            <ul>
    </HeaderTemplate>
    <ItemTemplate>
        <li></li>
    </ItemTemplate>
    <FooterTemplate>
        </ul> </div>
    </FooterTemplate>
</asp:Repeater>--%>

<div id="ad-album-inside" class="clearfix">
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
</div>
