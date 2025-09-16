<%@ Page Title="" Language="C#" MasterPageFile="~/News.master" AutoEventWireup="true" CodeFile="Sitemap.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/Modules/Categories/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
    <div class="box box-shadow box-content news-section sub-page">
        <section class="news-detail">
            <h3>Bản đồ Website <%= MMC.VTT.Properties.WEB_CONFIG.ApplicationName.ToString() %></h3><hr/><br/>
            <uc1:Sitemap ID="Sitemap1" runat="server" />
        </section>
    </div>    
</asp:Content>

