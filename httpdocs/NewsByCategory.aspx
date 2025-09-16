<%@ Page Language="C#" MasterPageFile="~/News.master" AutoEventWireup="true" CodeFile="NewsByCategory.aspx.cs"
    Inherits="NewsByCategory" Title="" %>
<%@ Register Src="Modules/Categories/BreakCrumb.ascx" TagName="BreakCrumb" TagPrefix="uc12" %>
<%@ Register Src="Modules/Articles/ArticleList.ascx" TagName="ArticleList" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Breadcrumbs -->
	<section class="breadcrumb-subpage clearfix">
		<uc12:breakcrumb id="BreakCrumb1" runat="server" />
	</section>	
    <!-- /Breadcrumbs -->

    <uc1:ArticleList ID="ArticleList1" runat="server" />
</asp:Content>
