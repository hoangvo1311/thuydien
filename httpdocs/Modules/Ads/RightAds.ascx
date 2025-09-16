<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RightAds.ascx.cs" Inherits="Modules_Ads_RightAds" %>

    <asp:Repeater ID="rptAds" runat="server">
        <ItemTemplate>
            <a href='Ads.aspx?AdsID=<%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Ads.ColumnNames.AdsID) %>'
                target="_blank">
                <img class="img-thumbnail2 img-responsive" src='<%= MMC.VTT.Properties.WEB_CONFIG.ApplicationPath %><%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Ads.ColumnNames.AdsImage) %>' />
            </a>
        </ItemTemplate>
    </asp:Repeater>

