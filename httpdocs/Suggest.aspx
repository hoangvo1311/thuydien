<%@ Page Language="C#" MasterPageFile="~/News.master" AutoEventWireup="true" CodeFile="Suggest.aspx.cs" Inherits="Suggest" Title="" %>

<%@ Register Src="Modules/Suggest/Suggest.ascx" TagName="Suggest" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <uc1:Suggest ID="Suggest1" runat="server" />
</asp:Content>

