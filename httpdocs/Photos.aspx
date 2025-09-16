<%@ Page Language="C#" MasterPageFile="~/News.master" AutoEventWireup="true" CodeFile="Photos.aspx.cs" Inherits="Photos" Title="" %>
<%@ Register Src="Modules/Gallery/PhotoList.ascx" TagName="PhotoList" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Breadcrumbs -->
	<section class="breadcrumb-subpage clearfix">
		<ol class="breadcrumb primary-bg">
            <li><a href="<%= MMC.VTT.Properties.WEB_CONFIG.ApplicationPath %>">Trang chủ</a></li>
            <li><a href="Gallery.aspx">Thư viện hình ảnh</a></li>
            <li><a href="#"><asp:Literal ID="lblAlbumName" runat="server"></asp:Literal></a></li>
        </ol>
	</section>	
    <!-- /Breadcrumbs -->

    <div id="album-inside">
        <uc1:PhotoList id="PhotoList1" runat="server"></uc1:PhotoList>
    </div>
</asp:Content>
