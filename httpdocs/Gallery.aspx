<%@ Page Language="C#" MasterPageFile="~/News.master" AutoEventWireup="true" CodeFile="Gallery.aspx.cs"
    Inherits="Gallery" Title="" %>
<%@ Register Src="Modules/Gallery/AlbumList.ascx" TagName="AlbumList" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="news-section clearfix">
        <h4 class="mod-title">Thư viện hình ảnh</h4>
        <br/>        
        <uc1:AlbumList ID="AlbumList1" runat="server"></uc1:AlbumList>
    </div>
</asp:Content>
