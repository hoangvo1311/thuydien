<%@ Page Language="C#" MasterPageFile="~/News.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="" %>
<%@ Register Src="Modules/Articles/HeadLine_Photo.ascx" TagName="HeadLine" TagPrefix="uc1" %>
<%@ Register Src="Modules/Articles/Home_Articles_Photo.ascx" TagName="HomeArticles" TagPrefix="uc2" %>
<%--<%@ Register Src="Modules/Articles/Home_GioiThieu.ascx" TagName="Home_GioiThieu" TagPrefix="uc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<uc1:Home_GioiThieu ID="Home_GioiThieu1" runat="server" />--%>
    <uc1:HeadLine ID="HeadLine1" runat="server" />
    <uc2:HomeArticles id="HomeArticles1" runat="server"></uc2:HomeArticles>
</asp:Content>

