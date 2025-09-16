<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeadLine_Photo.ascx.cs" Inherits="Modules_Articles_HeadLine" %>
<div id="home-headline" class="box box-shadow box-content clearfix">
    <div class="row">
        <div class="col-sm-8">
            <div id="big-headline" class="clearfix">
                <div id="headlineCarousel" class="carousel" data-ride="carousel">
                    <%--<div class="carousel-inner">
                        <asp:PlaceHolder ID="headline_Carousel" runat="server"></asp:PlaceHolder>
                    </div>--%>
                    
                <asp:Repeater ID="rpt" runat="server">
                    <HeaderTemplate>
                         <div class="carousel-inner">   
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="item <%# GetItemClass(Container.ItemIndex) %>">          
			                <img src="<%# GetImageOfArticle( int.Parse(DataBinder.Eval(Container.DataItem, "ArticleID").ToString()) ) %>">
                            
			                <div class="carousel-caption">
				                <div class="caption-big">
					                <h4><%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL._vtt_Articles.ColumnNames.Title)%></h4> 
				                </div>                                
			                </div>
		                </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>	                
	                <a class="left carousel-control" href="#headlineCarousel" data-slide="prev"><span class="glyphicon glyphicon-chevron-left"></span></a>
	                <a class="right carousel-control" href="#headlineCarousel" data-slide="next"><span class="glyphicon glyphicon-chevron-right"></span></a>		
                </div>

            </div>
        </div>
        <div class="col-sm-4">
            <ul class="list-unstyled headline-section">
                <asp:PlaceHolder ID="headline_section" runat="server"></asp:PlaceHolder>
            </ul>
        </div>
    </div>
</div>
