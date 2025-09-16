<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubMain.ascx.cs" Inherits="Modules_Ads_SubMain" %>
<div id="homeCarousel" class="carousel fade" data-ride="carousel">
	<!-- Indicators -->
	<ol class="carousel-indicators">
		<%--<li data-target="#homeCarousel" data-slide-to="0" class="active"></li>
		<li data-target="#homeCarousel" data-slide-to="1"></li> 
		<li data-target="#homeCarousel" data-slide-to="2"></li>--%>
        <asp:Repeater ID="indicatorRepeater" runat="server">
            <ItemTemplate>
                <li data-target="#homeCarousel" data-slide-to="<%# Container.ItemIndex %>" class="<%# IndicatorClass(Container.ItemIndex)%>"></li>
            </ItemTemplate>
        </asp:Repeater>
	</ol>
<asp:Repeater ID="rpt" runat="server">
    <HeaderTemplate>
         <div class="carousel-inner">   
    </HeaderTemplate>
    <ItemTemplate>
        <div class="item <%# GetItemClass(Container.ItemIndex) %>">          
			<img class="carousel-bg" src="<%# MMC.VTT.Properties.WEB_CONFIG.ApplicationPath + DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Ads.ColumnNames.AdsImage)%>" alt="" />
			<div class="carousel-caption">
				<div class="caption-big animated fadeInRight">
					<h4><%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Ads.ColumnNames.LinkName)%></h4> 
				</div>
                <div class="caption-desc animated fadeInLeft hidden">
					<%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Ads.ColumnNames.LinkName)%>
				</div>
			</div>
		</div>
   </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>
	<!-- Slideshow Next/Previous Controls -->
	<a class="left carousel-control" href="#homeCarousel" data-slide="prev"><span class="glyphicon glyphicon-chevron-left"></span></a>
	<a class="right carousel-control" href="#homeCarousel" data-slide="next"><span class="glyphicon glyphicon-chevron-right"></span></a>		
</div>

                   