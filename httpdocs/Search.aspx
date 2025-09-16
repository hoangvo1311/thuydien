<%@ Page Language="C#" MasterPageFile="~/News.master" AutoEventWireup="true" CodeFile="Search.aspx.cs"
    Inherits="Search" Title="" %>

<%@ Register Src="Modules/Articles/SearchResult.ascx" TagName="SearchResult" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="p-breakcrumb">
        <ul><li><a>Kết quả tìm kiếm</a></li></ul>
    </div>
    <uc2:SearchResult ID="SearchResult1" runat="server" />
</asp:Content>
