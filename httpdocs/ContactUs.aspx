<%@ Page Language="C#" MasterPageFile="~/News.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" Title="" %>

<%@ Register Src="Modules/ContactUs/ContactUs.ascx" TagName="ContactUs" TagPrefix="uc1" %>
<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="news-section clearfix">
        <h4 class="mod-title">Liên hệ và góp ý</h4>
        <br/>
        <p>Ban biên tập xin chân thành cảm ơn các ý kiến đóng góp của đọc giả, chúng tôi sẽ trả lời mọi góp ý trong thời gian sớm nhất.</p>
        <hr/>    
        <uc1:ContactUs ID="ContactUs1" runat="server" />
    </div>
    
</asp:Content>
